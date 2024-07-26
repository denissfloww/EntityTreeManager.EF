using EntityTreeManager.EF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityTreeManager.EF.Infrastructure;

public class TreeEntityConfiguration<TNode, TId> : IEntityTypeConfiguration<TNode> where TNode : TreeNode<TId>
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