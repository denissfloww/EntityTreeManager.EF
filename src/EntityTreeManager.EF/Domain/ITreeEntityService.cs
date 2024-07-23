namespace EntityTreeManager.EF.Domain;

public interface ITreeEntityService<TEntity, TId> where TEntity : TreeEntity<TId>
    where TId : struct
{
    IQueryable<TreeEntity<TId>> GetRoots();

    IQueryable<TreeEntity<TId>> GetChildren(TId id);

    IQueryable<TreeEntity<TId>> GetChildren(TreeEntity<TId> node);

    Task<TreeEntity<TId>?> GetByIdAsync(TId id);

    Task<TreeEntity<TId>?> GetParentAsync(TreeEntity<TId> node);

    Task<TreeEntity<TId>?> GetParentAsync(TId id);

    Task AttachParentAsync(TId childId, TId parentId);

    Task DetachParentAsync(TId childId);

    Task DetachNodeWithChildrenReassignment(TId nodeId);
}