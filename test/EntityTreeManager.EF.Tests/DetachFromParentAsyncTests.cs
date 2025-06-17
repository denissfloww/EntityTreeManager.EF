using EntityTreeManager.EF.Tests.TestUtilities;
using FluentAssertions;

namespace EntityTreeManager.EF.Tests;

public class DetachFromParentAsyncTests : TestBase
{
    [Theory]
    [InlineData(6)]
    [InlineData(4)]
    public async Task DetachFromParentAsync_ByChildIdExistParent_ShouldDetach(int childId)
    {
        await _treeService.DetachFromParentAsync(childId);
        
        var childNode = await _treeService.GetByIdAsync(childId);
        
        childNode.Should().NotBeNull();
        childNode.ParentId.Should().BeNull();
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public async Task DetachFromParentAsync_ByChildIdNonExistParent_NotThrowsException(int childId)
    {
        var act = async () => await _treeService.DetachFromParentAsync(childId);
        await act.Should().NotThrowAsync();
    }
}