using TreeFlow.EF.Tests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace TreeFlow.EF.Tests;

public class MoveToAsyncTests : TestBase
{
    [Theory]
    [InlineData(5, 4)]
    [InlineData(7, 5)]
    public async Task MoveToAsync_AttachesCorrectly(int childId, int parentId)
    {
        await TreeNodeManager.MoveToAsync(childId, parentId);
        await DbContext.SaveChangesAsync();
        
        var parentNode = await DbContext.TestTreeEntities
            .Include(n => n.Children)
            .FirstOrDefaultAsync(n => n.Id == parentId);

        parentNode.Should().NotBeNull();
        parentNode.Children.Should().NotBeNull();
        parentNode.Children.Select(n => n.Id).Should().Contain(childId);
    }
    
    [Theory]
    [InlineData(19999)]
    [InlineData(10000)]
    public async Task MoveToAsync_TryAttachNonExistent_ReturnsNull(int childId)
    {
        const int parentId = 6;
        
        await TreeNodeManager.MoveToAsync(childId, parentId);
        var parentNode = await DbContext.TestTreeEntities
            .Include(n => n.Children)
            .FirstOrDefaultAsync(n => n.Id == parentId);
        
        parentNode.Should().NotBeNull();
        parentNode.Children.Should().BeEmpty();
    }
    
    [Theory]
    [InlineData(6)]
    [InlineData(4)]
    public async Task MoveToAsync_ByChildIdExistParent_ShouldDetach(int childId)
    {
        await TreeNodeManager.MoveToAsync(childId, null);;
        
        var childNode = await TreeNodeManager.GetByIdAsync(childId);
        
        childNode.Should().NotBeNull();
        childNode.ParentId.Should().BeNull();
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public async Task MoveToAsync_ByChildIdNonExistParent_NotThrowsException(int childId)
    {
        var act = async () => await TreeNodeManager.MoveToAsync(childId, null);
        await act.Should().NotThrowAsync();
    }
}