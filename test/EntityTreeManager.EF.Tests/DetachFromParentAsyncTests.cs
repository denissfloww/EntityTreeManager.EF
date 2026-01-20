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
        await TreeNodeManager.DetachFromParentAsync(childId);
        
        var childNode = await TreeNodeManager.GetByIdAsync(childId);
        
        childNode.Should().NotBeNull();
        childNode.ParentId.Should().BeNull();
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public async Task DetachFromParentAsync_ByChildIdNonExistParent_NotThrowsException(int childId)
    {
        var act = async () => await TreeNodeManager.DetachFromParentAsync(childId);
        await act.Should().NotThrowAsync();
    }
}