using TreeFlow.EF.Tests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TreeFlow.EF.Core.Extensions;

namespace TreeFlow.EF.Tests;

public class WhereChildrenOfTests : TestBase
{
    [Theory]
    [InlineData(1, new[] {3, 4})]
    [InlineData(3, new[] {5})]
    [InlineData(4, new int[] { })]
    public async Task WhereChildrenOf_ByRootNodeId_ReturnsChildren(int parentId, int[] expectedNodeIds)
    {
        var children = await DbContext.TestTreeNodes
            .WhereChildrenOf(parentId)
            .ToListAsync();
        
        children.Should().NotBeNull();
        children.Select(c => c.Id).Should().Equal(expectedNodeIds);
    }
}

