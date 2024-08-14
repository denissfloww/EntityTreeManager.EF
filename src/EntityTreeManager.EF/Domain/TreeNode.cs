namespace EntityTreeManager.EF.Domain;

//TODO: Replace with an ITreeNode
/// <summary>
/// Represents a basic tree node structure with a unique identifier, 
/// a reference to its parent node, and a collection of child nodes.
/// </summary>
/// <typeparam name="TId">
/// The type of the node's identifier. It must be a value type.
/// </typeparam>
/// <remarks>
/// This abstract class serves as a base for implementing hierarchical EntityFramework entity.
/// </remarks>
public abstract class TreeNode<TId> where TId : struct
{
    /// <summary>
    /// Returns unique identifier of the node.
    /// </summary>
    public TId Id { get; init; }

    /// <summary>
    /// Gets or sets identifier of the parent node.
    /// </summary>
    public TId? ParentId { get; set; }
    
    /// <summary>
    /// Gets or sets the reference to the parent node.
    /// </summary>
    public TreeNode<TId>? Parent { get; set; }

    /// <summary>
    /// Gets or sets the collection of child nodes.
    /// </summary>
    public IEnumerable<TreeNode<TId>>? Children { get; set; }

    /// <summary>
    /// Gets a value indicating whether the node has any child nodes.
    /// </summary>
    public bool HasChildren => Children != null && Children.Any();
}