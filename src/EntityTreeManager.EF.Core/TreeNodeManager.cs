using EntityTreeManager.EF.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Core;

public class TreeNodeManager<TNode, TId>(
    DbContext dbContext) : ITreeNodeManager<TNode, TId> 
    where TNode : class, ITreeNode<TNode, TId>
    where TId : struct
{
    private DbSet<TNode> TreeSet => dbContext.Set<TNode>();

    public IQueryable<TNode> GetRoots()
    {
        return TreeSet.Where(c => c.ParentId == null).Include(c => c.Children);
    }

    public IQueryable<TNode> GetChildren(TId id)
    {
        return TreeSet.Where(c => c.ParentId != null &&
                                  EqualityComparer<TId>.Default.Equals(c.ParentId.Value,
                                      id))
            .Include(c => c.Parent)
            .Include(c => c.Children);
    }

    public IQueryable<TNode> GetChildren(TNode node)
    {
        ArgumentNullException.ThrowIfNull(node, nameof(node));

        return TreeSet.Where(c => c.Parent == node)
            .Include(c => c.Parent)
            .Include(c => c.Children);
    }

    public async Task<TNode?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await TreeSet.Include(c => c.Parent)
            .Include(c => c.Children)
            .FirstOrDefaultAsync(i => EqualityComparer<TId>.Default.Equals(i.Id,
                id), cancellationToken);
    }
    
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

    public async Task AttachParentAsync(TId nodeId, TId parentId, CancellationToken cancellationToken = default)
    {
        var child = await GetByIdAsync(nodeId, cancellationToken);
        if (child != null)
        {
            child.ParentId = parentId;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DetachFromParentAsync(TId nodeId, CancellationToken cancellationToken = default)
    {
        var child = await GetByIdAsync(nodeId, cancellationToken);
        if (child != null)
        {
            child.ParentId = null;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DetachFromTreeAsync(TId nodeId, CancellationToken cancellationToken = default)
    {
        var node = await GetByIdAsync(nodeId, cancellationToken);
        if (node != null)
        {
            var children = GetChildren(nodeId).ToList();

            foreach (var child in children)
            {
                child.ParentId = node.ParentId;
                dbContext.Update(child);
            }

            await DetachFromParentAsync(nodeId, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

