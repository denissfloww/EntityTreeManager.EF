using TreeFlow.EF.Abstractions;

namespace TreeFlow.EF.Tests.TestUtilities;

public class TestTreeNode : ITreeNode<TestTreeNode, int>
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public TestTreeNode? Parent { get; set; }
    public IEnumerable<TestTreeNode>? Children { get; set; } = new List<TestTreeNode>();
}
