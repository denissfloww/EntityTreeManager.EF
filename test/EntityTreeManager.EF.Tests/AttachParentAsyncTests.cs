using System.Diagnostics;
using EntityTreeManager.EF.Tests.TestUtilities;

namespace EntityTreeManager.EF.Tests;

public class AttachParentAsyncTests : TestBase
{
    [Fact]
    public async void AttachParentAsync_ReturnsSuccessful()
    {
        const int childId = 5;
        const int parentId = 4;
        
        await _treeService.AttachParentAsync(childId, parentId);
        var parentNode = await _dbContext.TestTreeEntities.FindAsync(parentId);

        Assert.NotNull(parentNode?.Children);
        Assert.Contains(childId, parentNode.Children.Select(n => n.Id));
    }
    
    [Fact]
    public async void AttachParentAsync_TryAttachNonExistent_ReturnsNull()
    {
        const int childId = 100000;
        const int parentId = 6;
        
        await _treeService.AttachParentAsync(childId, parentId);
        var parentNode = await _dbContext.TestTreeEntities.FindAsync(parentId);

        Assert.Null(parentNode?.Children);
    }
}