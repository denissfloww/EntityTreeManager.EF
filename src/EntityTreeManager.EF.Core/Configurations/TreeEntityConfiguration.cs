using EntityTreeManager.EF.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityTreeManager.EF.Core.Configurations;

/// <summary>
/// Configures the entity type mapping for a tree node entity in the Entity Framework model.
/// </summary>
/// <typeparam name="TNode">
/// The type of the tree node, which must implement <see cref="ITreeNode{TNode,TId}"/>.
/// </typeparam>
/// <typeparam name="TId">
/// The type of the tree node identifier, which must be a value type.
/// </typeparam>
public class TreeEntityConfiguration<TNode, TId> : IEntityTypeConfiguration<TNode> 
    where TNode : class, ITreeNode<TNode, TId>
    where TId : struct
{
    public void Configure(EntityTypeBuilder<TNode> builder)
    {
        builder.HasKey(ent => ent.Id);
        builder
            .HasOne(c => c.Parent)
            .WithMany(c => c.Children as IEnumerable<TNode>)
            .HasForeignKey(c => c.ParentId);
    }
}