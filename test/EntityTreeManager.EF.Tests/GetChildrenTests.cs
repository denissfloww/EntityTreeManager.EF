using EntityTreeManager.EF.Tests.TestUtilities;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Tests;

public class GetChildrenTests : TestBase
{
    [Fact]
    public async void GetChildren_ByRootNodeId_ReturnsChildren()
    {
        var children = await _treeService.GetChildren(1).ToListAsync();

        Assert.NotNull(children);
        Assert.Equal(2, children.Count);
        Assert.Equal(children.Select(c => c.Id), [3, 4]);
    }

    [Fact]
    public async void GetChildren_ByRootNode_ReturnsChildren()
    {
        var rootEntity = await _dbContext.TestTreeEntities.FindAsync(1);
        Assert.NotNull(rootEntity);

        var children = await _treeService.GetChildren(rootEntity).ToListAsync();

        Assert.NotNull(rootEntity);
        Assert.Equal(2, children.Count);
        Assert.Equal(children.Select(c => c.Id), [3, 4]);
    }

    [Fact]
    public async void GetChildren_ByLeafNodeId_ReturnsChildren()
    {
        var children = await _treeService.GetChildren(3).ToListAsync();

        Assert.NotNull(children);
        Assert.Single(children);
        Assert.Equal(children.Select(c => c.Id), [5]);
    }

    [Fact]
    public async void GetChildren_ByLeafNode_ReturnsChildren()
    {
        var rootEntity = await _dbContext.TestTreeEntities.FindAsync(3);
        Assert.NotNull(rootEntity);

        var children = await _treeService.GetChildren(rootEntity).ToListAsync();

        Assert.NotNull(rootEntity);
        Assert.Single(children);
        Assert.Equal(children.Select(c => c.Id), [5]);
    }

    [Fact]
    public async void GetChildren_NoChildren_ReturnsEmpty()
    {
        var children = await _treeService.GetChildren(4).ToListAsync();
        Assert.Empty(children);
    }
}

