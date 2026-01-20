using EntityTreeManager.EF.Tests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Tests;

public class GetRootsTestsTests : TestBase
{
    [Fact]
    public async Task GetRoots_ReturnsAllRoots()
    {
        var roots = await TreeNodeManager.GetRoots().ToListAsync();

        roots.Select(r => r.Id).Should().Equal([1, 2]);
    }
}