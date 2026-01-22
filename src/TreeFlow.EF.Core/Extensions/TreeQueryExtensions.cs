using TreeFlow.EF.Abstractions;

namespace TreeFlow.EF.Core.Extensions;

public static class TreeQueryExtensions
{
    public static IQueryable<TNode> WhereChildrenOf<TNode, TId>(this IQueryable<TNode> query,
        TId parentId)
        where TNode : class, ITreeNode<TNode, TId>
        where TId : struct
    {
        var comparer = EqualityComparer<TId>.Default;
        return query.Where(n => n.ParentId.HasValue &&
                                comparer.Equals(n.ParentId.Value, parentId));
    }
}










