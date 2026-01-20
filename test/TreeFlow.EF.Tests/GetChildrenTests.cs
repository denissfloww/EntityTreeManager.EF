using TreeFlow.EF.Tests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace TreeFlow.EF.Tests;

public class GetChildrenTests : TestBase
{
    [Theory]
    [InlineData(1, new[] {3, 4})]
    [InlineData(3, new[] {5})]
    [InlineData(4, new int[] { })]
    public async Task GetChildren_ByRootNodeId_ReturnsChildren(int parentId, int[] expectedNodeIds)
    {
        var children = await TreeNodeManager.GetChildren(parentId).ToListAsync();

        children.Should().NotBeNull();
        children.Select(c => c.Id).Should().Equal(expectedNodeIds);
    }

    [Theory]
    [InlineData(1, new[] {3, 4})]
    [InlineData(3, new[] {5})]
    [InlineData(4, new int[] { })]
    public async Task GetChildren_ByRootNode_ReturnsChildren(int parentId, int[] expectedNodeIds)
    {
        var rootEntity = await DbContext.TestTreeEntities.FindAsync(parentId);
        rootEntity.Should().NotBeNull();

        var children = await TreeNodeManager.GetChildren(rootEntity).ToListAsync();
        children.Select(c => c.Id).Should().Equal(expectedNodeIds);
    }

    [Fact]
    public async Task GetChildren_ByRootNode_ThrowNullException()
    {
        var act = () => TreeNodeManager.GetChildren(null!).ToListAsync();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}

