using EntityTreeManager.EF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityTreeManager.EF.Infrastructure;

public class TreeEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity> where TEntity : TreeEntity<TId>
    where TId : struct
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(ent => ent.Id);
        builder
            .HasOne(c => c.Parent)
            .WithMany(c => c.Children as IEnumerable<TEntity>)
            .HasForeignKey(c => c.ParentId);
    }
}