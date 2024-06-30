using EntityTreeManager.EF.Domain;

namespace EntityTreeManager.EF.Infrastructure;

public class TreeEntityService<TEntity, TId> : ITreeEntityService<TEntity, TId>
    where TEntity : TreeEntity<TId> where TId : struct
{
    public IQueryable<TreeEntity<TId>> GetRoots()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TreeEntity<TId>> GetChildren(TId id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TreeEntity<TId>> GetChildren(TreeEntity<TId> category)
    {
        throw new NotImplementedException();
    }

    public Task<TreeEntity<TId>?> GetByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public Task<TreeEntity<TId>?> GetParentAsync(TreeEntity<TId> category)
    {
        throw new NotImplementedException();
    }

    public Task<TreeEntity<TId>?> GetParentAsync(TId categoryId)
    {
        throw new NotImplementedException();
    }
}

