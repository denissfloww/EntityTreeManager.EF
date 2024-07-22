namespace EntityTreeManager.EF.Domain;

public interface ITreeEntity<TId> where TId : struct
{
    TId Id { get; set; }

    TId? ParentId { get; set; }

    ITreeEntity<TId>? Parent { get; set; }

    IEnumerable<ITreeEntity<TId>>? Children { get; set; }

    bool HasChildren => Children != null && Children.Any();
}
