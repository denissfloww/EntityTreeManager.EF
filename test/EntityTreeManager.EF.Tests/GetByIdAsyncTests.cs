using EntityTreeManager.EF.Tests.TestUtilities;
using FluentAssertions;

namespace EntityTreeManager.EF.Tests;

public class GetByIdAsyncTests : TestBase
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(7)]
    [InlineData(6)]
    public async Task GetById_ByExistId_ReturnsNode(int nodeId)
    {
        var node = await TreeNodeManager.GetByIdAsync(nodeId);
        node.Should().NotBeNull();
    }

    [Theory]
    [InlineData(110)]
    [InlineData(8)]
    public async Task GetById_NonExistingId_ReturnsNull(int nodeId)
    {
        var node = await TreeNodeManager.GetByIdAsync(nodeId);
        node.Should().BeNull();
    }
}

