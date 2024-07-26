namespace EntityTreeManager.EF.Domain;

//TODO: Write code description
public interface ITreeService<TNode, TId> where TNode : TreeNode<TId>
    where TId : struct
{
    IQueryable<TreeNode<TId>> GetRoots();

    IQueryable<TreeNode<TId>> GetChildren(TId id);

    IQueryable<TreeNode<TId>> GetChildren(TreeNode<TId> node);

    Task<TreeNode<TId>?> GetByIdAsync(TId id);

    Task<TreeNode<TId>?> GetParentAsync(TreeNode<TId> node);

    Task<TreeNode<TId>?> GetParentAsync(TId id);

    Task AttachParentAsync(TId nodeId, TId parentId);

    Task DetachFromParentAsync(TId nodeId);

    Task DetachFromTreeAsync(TId nodeId);
}