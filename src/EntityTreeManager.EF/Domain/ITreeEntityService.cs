namespace EntityTreeManager.EF.Domain;

public interface ITreeEntityService<TEntity, TId> where TEntity : TreeEntity<TId>
    where TId : struct
{
    IQueryable<TreeEntity<TId>> GetRoots();

    IQueryable<TreeEntity<TId>> GetChildren(TId id);

    IQueryable<TreeEntity<TId>> GetChildren(TreeEntity<TId> category);

    Task<TreeEntity<TId>?> GetByIdAsync(TId id);

    Task<TreeEntity<TId>?> GetParentAsync(TreeEntity<TId> category);

    Task<TreeEntity<TId>?> GetParentAsync(TId categoryId);
}