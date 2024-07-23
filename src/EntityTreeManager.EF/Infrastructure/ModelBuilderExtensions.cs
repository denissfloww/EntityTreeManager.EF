using EntityTreeManager.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Infrastructure;

public static class ModelBuilderExtensions
{
    public static ModelBuilder UseTreeEntityConfiguration<TEntity, TId>(this ModelBuilder modelBuilder)
        where TEntity : TreeEntity<TId>
        where TId : struct
    {
        modelBuilder.ApplyConfiguration(new TreeEntityConfiguration<TEntity, TId>());
        return modelBuilder;
    }
}