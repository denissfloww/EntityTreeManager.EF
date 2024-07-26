using EntityTreeManager.EF.Tests.TestUtilities;

namespace EntityTreeManager.EF.Tests;

public class DetachFromParentAsyncTests : TestBase
{
    [Fact]
    public async void DetachFromParentAsync_ByChildIdExistParent_ShouldDetach()
    {
        const int childId = 6;

        await _treeService.DetachFromParentAsync(childId);

        var childNode = await _treeService.GetByIdAsync(childId);
        Assert.NotNull(childNode);
        Assert.Null(childNode.ParentId);
    }

    [Fact]
    public async Task DetachFromParentAsync_ByChildIdNonExistParent_NotThrowsException()
    {
        const int childId = 2;
        try
        {
            await _treeService.DetachFromParentAsync(childId);
        }
        catch (Exception ex)
        {
            Assert.Fail("Expected no exception, but got: " + ex.Message);
        }
    }
}