namespace EntityTreeManager.EF.Domain;

/// <summary>
/// Defines a base interface for operations on tree nodes.
/// </summary>
/// <typeparam name="TNode">
/// The type of the tree node, which must inherit from <see cref="TreeNode{TId}"/>.
/// </typeparam>
/// <typeparam name="TId">
/// The type of the tree node identifier.
/// </typeparam>
/// <remarks>
/// This interface provides an abstraction for working with tree structures, 
/// offering methods for managing nodes as well as other operations related to hierarchical data structures.
/// </remarks>
public interface ITreeService<TNode, TId> where TNode : TreeNode<TId>
    where TId : struct
{
    /// <summary>
    /// Retrieves the root nodes of the tree.
    /// </summary>
    /// <returns>A <see cref="IQueryable{TNode}"/> collection of root nodes.</returns>
    IQueryable<TreeNode<TId>> GetRoots();

    /// <summary>
    /// Retrieves the child nodes of a specified parent node by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the parent node.</param>
    /// <returns>A <see cref="IQueryable{TNode}"/> collection of child nodes.</returns>
    IQueryable<TreeNode<TId>> GetChildren(TId id);

    /// <summary>
    /// Retrieves the child nodes of a specified parent node.
    /// </summary>
    /// <param name="node">The parent node.</param>
    /// <returns>A <see cref="IQueryable{TNode}"/> collection of child nodes.</returns>
    IQueryable<TreeNode<TId>> GetChildren(TreeNode<TId> node);

    /// <summary>
    /// Asynchronously retrieves a node by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the node.</param>
    /// <returns>A <see cref="IQueryable{TNode}"/> collection of child nodes.</returns>
    Task<TreeNode<TId>?> GetByIdAsync(TId id);

    /// <summary>
    /// Asynchronously retrieves the parent of the specified node.
    /// </summary>
    /// <param name="node">The node whose parent is to be retrieved.</param>
    /// <returns>
    /// A <see cref="Task{TNode}"/> that represents the asynchronous operation. 
    /// The task result contains the parent node if found; otherwise, <c>null</c>.
    /// </returns>
    Task<TreeNode<TId>?> GetParentAsync(TreeNode<TId> node);

    /// <summary>
    /// Asynchronously retrieves the parent of the node specified by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the node whose parent is to be retrieved.</param>
    /// <returns>
    /// A <see cref="Task{TNode}"/> that represents the asynchronous operation. 
    /// The task result contains the parent node if found; otherwise, <c>null</c>.
    /// </returns>
    Task<TreeNode<TId>?> GetParentAsync(TId id);

    /// <summary>
    /// Asynchronously attaches a node to a new parent.
    /// </summary>
    /// <param name="nodeId">The identifier of the node to be attached.</param>
    /// <param name="parentId">The identifier of the parent node.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task AttachParentAsync(TId nodeId, TId parentId);

    /// <summary>
    /// Asynchronously detaches a node from its current parent.
    /// </summary>
    /// <param name="nodeId">The identifier of the node to be detached.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task DetachFromParentAsync(TId nodeId);

    /// <summary>
    /// Asynchronously removes a node from the tree and reassigns its children to its parent node.
    /// </summary>
    /// <param name="nodeId">The identifier of the node to be removed.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task DetachFromTreeAsync(TId nodeId);
}