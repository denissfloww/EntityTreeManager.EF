using EntityTreeManager.EF.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Core;

public class TreeNodeManager<TNode, TId>(
    DbContext dbContext) : ITreeNodeManager<TNode, TId> 
    where TNode : class, ITreeNode<TNode, TId>
    where TId : struct
{
    private DbSet<TNode> TreeSet => dbContext.Set<TNode>();

    public IQueryable<TNode> GetRoots() => 
        TreeSet.Where(c => c.ParentId == null);

    public IQueryable<TNode> GetChildren(TId id) =>
        TreeSet.Where(c => c.ParentId != null &&
                           EqualityComparer<TId>.Default.Equals(c.ParentId.Value,
                               id));

    public IQueryable<TNode> GetChildren(TNode node)
    {
        ArgumentNullException.ThrowIfNull(node, nameof(node));
        return TreeSet.Where(c => c.Parent == node);
    }

    public async Task<TNode?> GetByIdAsync(TId id, CancellationToken cancellationToken = default) =>
        await TreeSet
            .FirstOrDefaultAsync(i => EqualityComparer<TId>.Default.Equals(i.Id,
                id), cancellationToken);

    public async Task<TNode?> GetParentAsync(TNode node, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(node, nameof(node));
        if (node.ParentId == null)
        {
            return null;
        }

        return await GetByIdAsync(node.ParentId.Value, cancellationToken);
    }

    public async Task<TNode?> GetParentAsync(TId id, CancellationToken cancellationToken = default)
    {
        var treeObject = await GetByIdAsync(id, cancellationToken);

        if (treeObject?.ParentId == null)
        {
            return null;
        }

        return await GetByIdAsync(treeObject.ParentId.Value, cancellationToken);
    }

    public async Task MoveToAsync(TId nodeId, TId? newParentId, CancellationToken cancellationToken = default)
    {
        var child = await GetByIdAsync(nodeId, cancellationToken);
        if (child != null)
        {
            child.ParentId = newParentId;
        }
    }
}

