namespace EntityTreeManager.EF.Domain;

public abstract class TreeEntity<TId> : ITreeEntity<TId> where TId : struct
{
    public TId Id { get; set; }

    public TId? ParentId { get; set; }
    public ITreeEntity<TId>? Parent { get; set; }

    public IEnumerable<ITreeEntity<TId>>? Children { get; set; }

    public bool HasChildren => Children != null && Children.Any();
}