using Microsoft.EntityFrameworkCore;

namespace TreeFlow.EF.Tests.TestUtilities;

public class TestTreeContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TestTreeNode> TestTreeEntities { get; set; }
}