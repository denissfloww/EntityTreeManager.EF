using EntityTreeManager.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Infrastructure;

public class TreeEntityService<TDbContext, TEntity, TId>(
    TDbContext dbContext) : ITreeEntityService<TEntity, TId> where TDbContext : DbContext
    where TEntity : TreeEntity<TId>
    where TId : struct
{
    private DbSet<TEntity> TreeSet => dbContext.Set<TEntity>();

    public IQueryable<TreeEntity<TId>> GetRoots()
    {
        return TreeSet.Where(c => c.ParentId == null).Include(c => c.Children);
    }

    public IQueryable<TreeEntity<TId>> GetChildren(TId id)
    {
        return TreeSet.Where(c => c.ParentId != null &&
                                  EqualityComparer<TId>.Default.Equals(c.ParentId.Value,
                                      id))
            .Include(c => c.Parent)
            .Include(c => c.Children);
    }

    public IQueryable<TreeEntity<TId>> GetChildren(TreeEntity<TId> node)
    {
        ArgumentNullException.ThrowIfNull(node, nameof(node));

        return TreeSet.Where(c => c.Parent == node)
            .Include(c => c.Parent)
            .Include(c => c.Children);
    }

    public async Task<TreeEntity<TId>?> GetByIdAsync(TId id)
    {
        return await TreeSet.Include(c => c.Parent)
            .Include(c => c.Children)
            .FirstOrDefaultAsync(i => EqualityComparer<TId>.Default.Equals(i.Id,
                id));
    }

    public async Task<TreeEntity<TId>?> GetParentAsync(TreeEntity<TId> node)
    {
        ArgumentNullException.ThrowIfNull(node, nameof(node));

        if (node.ParentId == null)
        {
            return null;
        }

        return await GetByIdAsync(node.ParentId.Value);
    }

    public async Task<TreeEntity<TId>?> GetParentAsync(TId id)
    {
        var treeObject = await GetByIdAsync(id);

        if (treeObject?.ParentId == null)
        {
            return null;
        }

        return await GetByIdAsync(treeObject.ParentId.Value);
    }

    public async Task AttachParentAsync(TId childId, TId parentId)
    {
        var child = await GetByIdAsync(childId);
        if (child != null)
        {
            child.ParentId = parentId;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task DetachParentAsync(TId childId)
    {
        var child = await GetByIdAsync(childId);
        if (child != null)
        {
            child.ParentId = null;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task DetachNodeWithChildrenReassignment(TId nodeId)
    {
        var node = await GetByIdAsync(nodeId);
        if (node != null)
        {
            var children = GetChildren(nodeId).ToList();

            foreach (var child in children)
            {
                child.ParentId = node.ParentId;
                dbContext.Update(child);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

