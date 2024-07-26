using EntityTreeManager.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Infrastructure;

public class TreeService<TDbContext, TNode, TId>(
    TDbContext dbContext) : ITreeService<TNode, TId> where TDbContext : DbContext
    where TNode : TreeNode<TId>
    where TId : struct
{
    private DbSet<TNode> TreeSet => dbContext.Set<TNode>();

    public IQueryable<TreeNode<TId>> GetRoots()
    {
        return TreeSet.Where(c => c.ParentId == null).Include(c => c.Children);
    }

    public IQueryable<TreeNode<TId>> GetChildren(TId id)
    {
        return TreeSet.Where(c => c.ParentId != null &&
                                  EqualityComparer<TId>.Default.Equals(c.ParentId.Value,
                                      id))
            .Include(c => c.Parent)
            .Include(c => c.Children);
    }

    public IQueryable<TreeNode<TId>> GetChildren(TreeNode<TId> node)
    {
        ArgumentNullException.ThrowIfNull(node, nameof(node));

        return TreeSet.Where(c => c.Parent == node)
            .Include(c => c.Parent)
            .Include(c => c.Children);
    }

    public async Task<TreeNode<TId>?> GetByIdAsync(TId id)
    {
        return await TreeSet.Include(c => c.Parent)
            .Include(c => c.Children)
            .FirstOrDefaultAsync(i => EqualityComparer<TId>.Default.Equals(i.Id,
                id));
    }

    public async Task<TreeNode<TId>?> GetParentAsync(TreeNode<TId> node)
    {
        ArgumentNullException.ThrowIfNull(node, nameof(node));

        if (node.ParentId == null)
        {
            return null;
        }

        return await GetByIdAsync(node.ParentId.Value);
    }

    public async Task<TreeNode<TId>?> GetParentAsync(TId id)
    {
        var treeObject = await GetByIdAsync(id);

        if (treeObject?.ParentId == null)
        {
            return null;
        }

        return await GetByIdAsync(treeObject.ParentId.Value);
    }

    public async Task AttachParentAsync(TId nodeId, TId parentId)
    {
        var child = await GetByIdAsync(nodeId);
        if (child != null)
        {
            child.ParentId = parentId;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task DetachFromParentAsync(TId nodeId)
    {
        var child = await GetByIdAsync(nodeId);
        if (child != null)
        {
            child.ParentId = null;
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task DetachFromTreeAsync(TId nodeId)
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

            await DetachFromParentAsync(nodeId);
            await dbContext.SaveChangesAsync();
        }
    }
}

