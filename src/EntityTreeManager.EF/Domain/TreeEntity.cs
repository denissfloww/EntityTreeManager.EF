namespace EntityTreeManager.EF.Domain;

public abstract class TreeEntity<TId> where TId : struct
{
    public TId Id { get; set; }

    public TId? ParentId { get; set; }
    public TreeEntity<TId>? Parent { get; set; }

    public IList<TreeEntity<TId>>? Children { get; set; }

    public bool HasChildren => Children is { Count: > 0 };
}