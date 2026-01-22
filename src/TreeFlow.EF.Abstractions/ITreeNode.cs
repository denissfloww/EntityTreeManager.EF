namespace TreeFlow.EF.Abstractions;

/// <summary>
/// Defines the contract for a tree node with hierarchical parent-child relationships.
/// Self-referential generic allows navigation properties to use the concrete node type.
/// </summary>
/// <typeparam name="TNode">The concrete node type (usually the implementing class).</typeparam>
/// <typeparam name="TId">The CLR type used for entity identifiers (the type of the node Id and of the ParentId foreign key). Common choices: int, long, Guid, etc.</typeparam>
public interface ITreeNode<TNode, TId>
    where TNode : class, ITreeNode<TNode, TId>
    where TId : struct
{
    /// <summary>
    /// Gets or sets identifier of the parent node.
    /// </summary>
    TId? ParentId { get; set; }

    /// <summary>
    /// Navigation property to the parent node.
    /// </summary>
    TNode? Parent { get; set; }

    /// <summary>
    /// Navigation property to the children nodes.
    /// </summary>
    IEnumerable<TNode>? Children { get; set; }
}