using EntityTreeManager.EF.Abstractions;
using EntityTreeManager.EF.Core.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Core.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Applies the tree node entity configuration to the <see cref="ModelBuilder"/> instance.
    /// </summary>
    /// <typeparam name="TNode">
    /// The type of the tree node entity, which must inherit from <see cref="TreeNode{TId}"/>.
    /// </typeparam>
    /// <typeparam name="TId">
    /// The type of the tree node identifier, which must be a value type.
    /// </typeparam>
    /// <param name="modelBuilder">
    /// The <see cref="ModelBuilder"/> instance to which the configuration is applied.
    /// </param>
    /// <returns>
    /// The <see cref="ModelBuilder"/> instance with the applied configuration.
    /// </returns>
    /// <remarks>
    /// This extension method simplifies the process of applying the <see cref="TreeEntityConfiguration{TNode, TId}"/> 
    /// configuration to the Entity Framework model. It sets up the entity mappings for a tree structure, 
    /// including the primary key and self-referencing foreign key relationships.
    /// </remarks>
    public static ModelBuilder UseTreeConfiguration<TNode, TId>(this ModelBuilder modelBuilder)
        where TNode : TreeNode<TId>
        where TId : struct
    {
        modelBuilder.ApplyConfiguration(new TreeEntityConfiguration<TNode, TId>());
        return modelBuilder;
    }
}