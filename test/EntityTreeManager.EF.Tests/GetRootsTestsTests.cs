using EntityTreeManager.EF.Tests.TestUtilities;
using Microsoft.EntityFrameworkCore;

namespace EntityTreeManager.EF.Tests;

public class GetRootsTestsTests : TestBase
{
    [Fact]
    public async void GetRoots_ReturnsAllRoots()
    {
        var roots = await _treeService.GetRoots().ToListAsync();

        Assert.Equal([ 1, 2 ], roots.Select(r => r.Id));
    }
}