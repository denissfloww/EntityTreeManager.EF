using EntityTreeManager.EF.Tests.TestUtilities;
using FluentAssertions;

namespace EntityTreeManager.EF.Tests;

public class GetParentAsyncTests : TestBase
{
    [Theory]
    [InlineData(4, 1)]
    [InlineData(3, 1)]
    [InlineData(7, 5)]
    public async Task GetParentAsync_ByChildId_ReturnsParent(int childId, int expectedParentId)
    {
        var parent = await _treeService.GetParentAsync(childId);

        parent.Should().NotBeNull();
        parent!.Id.Should().Be(expectedParentId);
    }
    
    [Theory]
    [InlineData(5, 3)]
    [InlineData(4, 1)]
    [InlineData(3, 1)]
    [InlineData(7, 5)]
    public async Task GetParentAsync_ByChild_ReturnsParent(int childId, int expectedParentId)
    {
        var child = await _dbContext.TestTreeEntities.FindAsync(childId);
        child.Should().NotBeNull();

        var parent = await _treeService.GetParentAsync(child);
        
        parent.Should().NotBeNull();
        parent!.Id.Should().Be(expectedParentId);
    }


    [Theory]
    [InlineData(2)]
    [InlineData(1)]
    public async Task GetParentAsync_ByRootNodeId_ReturnsNull(int rootId)
    {
        var root = await _treeService.GetParentAsync(rootId);

        root.Should().BeNull();
    }
    
    [Theory]
    [InlineData(2)]
    [InlineData(1)]
    public async Task GetParentAsync_ByRootNode_ReturnsNull(int rootId)
    {
        var root = await _dbContext.TestTreeEntities.FindAsync(rootId);
        root.Should().NotBeNull();
        
        var parent = await _treeService.GetParentAsync(root);
        parent.Should().BeNull();
    }
}

