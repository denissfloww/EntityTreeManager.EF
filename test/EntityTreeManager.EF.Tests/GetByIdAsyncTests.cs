using EntityTreeManager.EF.Tests.TestUtilities;

namespace EntityTreeManager.EF.Tests;

public class GetByIdAsyncTests : TestBase
{
    [Fact]
    public async void GetById_ByExistId_ReturnsEntity()
    {

        
        var entity = await _treeService.GetByIdAsync(1);
        Assert.NotNull(entity);
    }

    [Fact]
    public async void GetById_NonExistingId_ReturnsNull()
    {
        var entity = await _treeService.GetByIdAsync(110);
        Assert.Null(entity);
    }
}

