using EntityTreeManager.EF.Core;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Tests.TestUtilities;

public class TestBase : IAsyncLifetime
{
    protected TestTreeContext DbContext { get; private set; } = null!;
    protected TreeNodeManager<TestTreeNode, int> TreeNodeManager { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        DbContext = GetDbContext();
        TreeNodeManager = new TreeNodeManager<TestTreeNode, int>(DbContext);

        await SeedDatabase();
    }

    private static TestTreeContext GetDbContext()
    {
        var builder = new DbContextOptionsBuilder<TestTreeContext>().UseInMemoryDatabase("TestTreeDb" + Guid.NewGuid());
        var dbContext = new TestTreeContext(builder.Options);

        return dbContext;
    }

    private async Task SeedDatabase()
    {
        var entities = new[]
        {
            new TestTreeNode { Id = 1 },
            new TestTreeNode { Id = 2 },
            new TestTreeNode { Id = 3, ParentId = 1 },
            new TestTreeNode { Id = 4, ParentId = 1 },
            new TestTreeNode { Id = 5, ParentId = 3 },
            new TestTreeNode { Id = 6, ParentId = 2 },
            new TestTreeNode { Id = 7, ParentId = 5 }
        };

        DbContext.AddRange(entities);
        await DbContext.SaveChangesAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}

