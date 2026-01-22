using Microsoft.EntityFrameworkCore;
using TreeFlow.EF.Abstractions;

namespace TreeFlow.EF.Core.Extensions;

public static class TreeDbSetExtensions
{
    public static async Task MoveToAsync<TNode, TId>(
        this DbSet<TNode> set,
        TId nodeId,
        TId? newParentId,
        CancellationToken cancellationToken = default)
        where TNode : class, ITreeNode<TNode, TId>
        where TId : struct
    {
        var node = await set.FindAsync(new object[] { nodeId }, cancellationToken);
        if (node == null)
            return;

        node.ParentId = newParentId;
    }
}