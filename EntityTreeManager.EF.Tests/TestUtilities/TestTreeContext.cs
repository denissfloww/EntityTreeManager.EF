using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Tests.TestUtilities;

public class TestTreeContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TestTreeEntity> TestTreeEntities { get; set; }
}