using EntityTreeManager.EF.Tests.TestUtilities;
using FluentAssertions;

namespace EntityTreeManager.EF.Tests;

public class DetachFromTreeAsyncTests : TestBase
{
    [Theory]
    [InlineData(1)]
    public async Task DetachFromTreeAsync_WhenNodeExists_ShouldReassignChildren(int detachedParentId)
    {
        var oldChildrenIds = TreeNodeManager.GetChildren(detachedParentId).Select(c => c.Id);
        await TreeNodeManager.DetachFromTreeAsync(detachedParentId);

        var children = DbContext.TestTreeEntities.Where(n => oldChildrenIds.Contains(n.Id)).ToList();
        children.Should().AllSatisfy(c => c.ParentId.Should().NotBe(detachedParentId));
    }
}