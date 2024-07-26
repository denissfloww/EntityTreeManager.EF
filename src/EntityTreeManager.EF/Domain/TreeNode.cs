namespace EntityTreeManager.EF.Domain;

//TODO: Replace with an ITreeNode
public abstract class TreeNode<TId> where TId : struct
{
    public TId Id { get; init; }

    public TId? ParentId { get; set; }
    public TreeNode<TId>? Parent { get; set; }

    public IEnumerable<TreeNode<TId>>? Children { get; set; }

    public bool HasChildren => Children != null && Children.Any();
}