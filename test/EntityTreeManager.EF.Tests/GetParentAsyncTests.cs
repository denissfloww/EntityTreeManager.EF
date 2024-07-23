using EntityTreeManager.EF.Tests.TestUtilities;

namespace EntityTreeManager.EF.Tests;

public class GetParentAsyncTests : TestBase
{
    [Fact]
    public async void GetParentAsync_ByLeafNodeId_ReturnsParent()
    {
        var parent = await _treeService.GetParentAsync(4);

        Assert.NotNull(parent);
        Assert.Equal(parent.Id, 1);
    }

    [Fact]
    public async void GetParentAsync_ByRootNodeId_ReturnsNull()
    {
        var parent = await _treeService.GetParentAsync(2);

        Assert.Null(parent);
    }

    [Fact]
    public async void GetParentAsync_ByLeafNode_ReturnsParent()
    {
        var leafEntity = await _dbContext.TestTreeEntities.FindAsync(5);
        Assert.NotNull(leafEntity);

        var parent = await _treeService.GetParentAsync(leafEntity);

        Assert.NotNull(parent);
        Assert.Equal(parent.Id, 3);
    }

    [Fact]
    public async void GetParentAsync_ByRootNode_ReturnsNull()
    {
        var rootEntity = await _dbContext.TestTreeEntities.FindAsync(1);
        Assert.NotNull(rootEntity);

        var parent = await _treeService.GetParentAsync(rootEntity);

        Assert.Null(parent);
    }
}

