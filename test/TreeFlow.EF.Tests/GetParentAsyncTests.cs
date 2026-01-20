using TreeFlow.EF.Tests.TestUtilities;
using FluentAssertions;

namespace TreeFlow.EF.Tests;

public class GetParentAsyncTests : TestBase
{
    [Theory]
    [InlineData(4, 1)]
    [InlineData(3, 1)]
    [InlineData(7, 5)]
    public async Task GetParentAsync_ByChildId_ReturnsParent(int childId, int expectedParentId)
    {
        var parent = await TreeNodeManager.GetParentAsync(childId);

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
        var child = await DbContext.TestTreeEntities.FindAsync(childId);
        child.Should().NotBeNull();

        var parent = await TreeNodeManager.GetParentAsync(child);
        
        parent.Should().NotBeNull();
        parent!.Id.Should().Be(expectedParentId);
    }


    [Theory]
    [InlineData(2)]
    [InlineData(1)]
    public async Task GetParentAsync_ByRootNodeId_ReturnsNull(int rootId)
    {
        var root = await TreeNodeManager.GetParentAsync(rootId);

        root.Should().BeNull();
    }
    
    [Theory]
    [InlineData(2)]
    [InlineData(1)]
    public async Task GetParentAsync_ByRootNode_ReturnsNull(int rootId)
    {
        var root = await DbContext.TestTreeEntities.FindAsync(rootId);
        root.Should().NotBeNull();
        
        var parent = await TreeNodeManager.GetParentAsync(root);
        parent.Should().BeNull();
    }
}

