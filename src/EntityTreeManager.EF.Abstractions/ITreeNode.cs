namespace EntityTreeManager.EF.Abstractions;

/// <summary>
/// Defines the contract for a tree node with hierarchical parent-child relationships.
/// </summary>
/// <typeparam name="TNode">The type of the tree node that implements this interface.</typeparam>
/// <typeparam name="TId">The type of the node identifier, which must be a value type.</typeparam>
public interface ITreeNode<TNode, TId> 
    where TNode : class
    where TId : struct
{
    /// <summary>
    /// Returns unique identifier of the node.
    /// </summary>
    TId Id { get; set; }

    /// <summary>
    /// Gets or sets identifier of the parent node.
    /// </summary>
    TId? ParentId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the parent node.
    /// </summary>
    TNode? Parent { get; set; }

    /// <summary>
    /// Gets or sets the collection of child nodes.
    /// </summary>
    IEnumerable<TNode>? Children { get; set; }
    
    /// <summary>
    /// Gets a value indicating whether the node has any child nodes.
    /// </summary>
    bool HasChildren => Children != null && Children.Any();
}
