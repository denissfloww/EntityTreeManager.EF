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

    public IQueryable<TreeEntity<TId>> GetChildren(TreeEntity<TId> treeObject)
    {
        return TreeSet.Where(c => c.Parent == treeObject)
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

    public async Task<TreeEntity<TId>?> GetParentAsync(TreeEntity<TId> treeObject)
    {
        if (treeObject?.ParentId == null)
        {
            return null;
        }

        return await GetByIdAsync(treeObject.ParentId.Value);
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
}

