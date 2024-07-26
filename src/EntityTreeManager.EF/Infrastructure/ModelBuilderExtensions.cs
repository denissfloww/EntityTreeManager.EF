using EntityTreeManager.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Infrastructure;

public static class ModelBuilderExtensions
{
    public static ModelBuilder UseTreeConfiguration<TNode, TId>(this ModelBuilder modelBuilder)
        where TNode : TreeNode<TId>
        where TId : struct
    {
        modelBuilder.ApplyConfiguration(new TreeEntityConfiguration<TNode, TId>());
        return modelBuilder;
    }
}