using Microsoft.EntityFrameworkCore;
using TreeFlow.EF.Core.Extensions;

namespace TreeFlow.EF.Tests.TestUtilities;

public class TestTreeContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TestTreeNode> TestTreeNodes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseTreeFlow<TestTreeNode, int>();
    }
}