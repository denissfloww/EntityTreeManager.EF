using EntityTreeManager.EF.Tests.TestUtilities;

namespace EntityTreeManager.EF.Tests;

public class DetachFromTreeAsyncTests : TestBase
{
    [Fact]
    public async Task DetachFromTreeAsync_ShouldReassignChildren_WhenNodeExists()
    {
        const int parentId = 5;
        const int newNodeId = 8;
        const int newParentId = 3;
        
        _dbContext.Add(new TestTreeNode { Id = newNodeId });
        await _dbContext.SaveChangesAsync();

        await _treeService.AttachParentAsync(newNodeId, parentId);
        await _treeService.DetachFromTreeAsync(parentId);

        var newParentNode = _treeService.GetChildren(newParentId);
        
        Assert.Equal(newParentNode.Select(c => c.Id), [7, 8]);
    }
}