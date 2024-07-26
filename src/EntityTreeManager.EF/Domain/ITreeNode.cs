namespace EntityTreeManager.EF.Domain;

public interface ITreeNode<TId> where TId : struct
{
    TId Id { get; set; }

    TId? ParentId { get; set; }

    ITreeNode<TId>? Parent { get; set; }

    IEnumerable<ITreeNode<TId>>? Children { get; set; }

    bool HasChildren => Children != null && Children.Any();
}
