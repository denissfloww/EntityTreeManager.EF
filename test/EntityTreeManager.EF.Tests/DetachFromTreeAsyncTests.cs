using EntityTreeManager.EF.Tests.TestUtilities;
using FluentAssertions;

namespace EntityTreeManager.EF.Tests;

public class DetachFromTreeAsyncTests : TestBase
{
    [Theory]
    [InlineData(1)]
    public async Task DetachFromTreeAsync_WhenNodeExists_ShouldReassignChildren(int detachedParentId)
    {
        var oldChildrenIds = _treeService.GetChildren(detachedParentId).Select(c => c.Id);
        await _treeService.DetachFromTreeAsync(detachedParentId);

        var children = _dbContext.TestTreeEntities.Where(n => oldChildrenIds.Contains(n.Id)).ToList();
        children.Should().AllSatisfy(c => c.ParentId.Should().NotBe(detachedParentId));
    }
}