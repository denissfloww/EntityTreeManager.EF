using EntityTreeManager.EF.Tests.TestUtilities;
using FluentAssertions;

namespace EntityTreeManager.EF.Tests;

public class AttachParentAsyncTests : TestBase
{
    [Theory]
    [InlineData(5, 4)]
    [InlineData(7, 5)]
    public async Task AttachParentAsync_AttachesCorrectly(int childId, int parentId)
    {
        await _treeService.AttachParentAsync(childId, parentId);
        var parentNode = await _dbContext.TestTreeEntities.FindAsync(parentId);

        parentNode.Should().NotBeNull();
        parentNode.Children.Should().NotBeNull();
        parentNode.Children.Select(n => n.Id).Should().Contain(childId);
    }
    
    [Theory]
    [InlineData(19999)]
    [InlineData(10000)]
    public async Task AttachParentAsync_TryAttachNonExistent_ReturnsNull(int childId)
    {
        const int parentId = 6;
        
        await _treeService.AttachParentAsync(childId, parentId);
        var parentNode = await _dbContext.TestTreeEntities.FindAsync(parentId);
        
        parentNode.Should().NotBeNull();
        parentNode.Children.Should().BeNull();
    }
}