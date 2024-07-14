using EntityTreeManager.EF.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Tests.TestUtilities;

public class TestBase : IAsyncLifetime
{
    protected TestTreeContext _dbContext { get; private set; }
    protected TreeEntityService<TestTreeContext, TestTreeEntity, int> _treeService { get; private set; }

    public async Task InitializeAsync()
    {
        _dbContext = GetDbContext();
        _treeService = new TreeEntityService<TestTreeContext, TestTreeEntity, int>(_dbContext);

        await SeedDatabase();
    }

    private static TestTreeContext GetDbContext()
    {
        var builder = new DbContextOptionsBuilder<TestTreeContext>().UseInMemoryDatabase("TreeTestDb" + Guid.NewGuid());
        var dbContext = new TestTreeContext(builder.Options);

        return dbContext;
    }

    private async Task SeedDatabase()
    {
        var entities = new[]
        {
            new TestTreeEntity { Id = 1 },
            new TestTreeEntity { Id = 2 },
            new TestTreeEntity { Id = 3, ParentId = 1 },
            new TestTreeEntity { Id = 4, ParentId = 1 },
            new TestTreeEntity { Id = 5, ParentId = 3 },
            new TestTreeEntity { Id = 6, ParentId = 2 },
            new TestTreeEntity { Id = 7, ParentId = 5 }
        };

        _dbContext.AddRange(entities);
        await _dbContext.SaveChangesAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}

