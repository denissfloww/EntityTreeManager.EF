Entity Tree Manager
===========
A library that encapsulates working with hierarchical entities and provides convenient tools for obtaining such entities using the Entity Famework

![build workflow](https://github.com/denissfloww/EntityTreeManager.EF/actions/workflows/build.yml/badge.svg)

Usage

An example of using basic classes:

```c#
using Entity;

public class ConcreteEntity : TreeNode<int>
{
    public int Id { get; set; }
    
    public int Name { get; set; }
}
```