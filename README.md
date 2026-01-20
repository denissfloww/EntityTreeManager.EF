Entity Tree Manager
===========
A library that encapsulates working with hierarchical entities and provides convenient tools for obtaining such entities using the Entity Famework

![build workflow](https://github.com/denissfloww/EntityTreeManager.EF/actions/workflows/build.yml/badge.svg)

## Overview

`TreeNodeManager<TDbContext, TNode, TId>` is a generic service for managing hierarchical tree structures within an Entity Framework context. This service provides methods for retrieving and manipulating tree nodes, including operations for attaching, detaching, and querying nodes and their relationships.

### Type Parameters

- **TDbContext**: The type of the Entity Framework `DbContext`.
- **TNode**: The type of the tree node, which must inherit from `TreeNode<TId>`.
- **TId**: The type of the node identifier, which must be a value type.

## Configuration
### Step 1: Define the Entity Class

```c#
public class Category : TreeNode<int>
{
    public int Id { get; set; }
    
    public int Name { get; set; }
}
```

### Step 2: Configure the Entity in DbContext

In your DbContext class, you need to configure the Category entity using `TreeEntityConfiguration`

```c#
public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Category entity
        modelBuilder.ApplyConfiguration(new TreeEntityConfiguration<Category>());
    }
}
```

Or use extension method

```c#
public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Category entity with extension method
        modelBuilder.UseTreeConfiguration<Category, int>();
    }
}
```

### Step 3: Dependency Registration for `ITreeNodeManager<TNode, TId>`

In your DbContext class, you need to configure the Category entity using `TreeEntityConfiguration`

```c#
builder.Services.AddScoped<ITreeNodeManager<Category, int>, TreeNodeManager<ApplicationDbContext, Category, int>>();
```

## Methods

### `GetRoots()`

Retrieves the root nodes of the tree.

**Returns**: `IQueryable<TreeNode<TId>>`  
A collection of root nodes.

### `GetChildren(TId id)`

Retrieves the child nodes of a specified parent node by its identifier.

**Parameters**:
- `id` (TId): The identifier of the parent node.

**Returns**: `IQueryable<TreeNode<TId>>`  
A collection of child nodes.

### `GetChildren(TreeNode<TId> node)`

Retrieves the child nodes of a specified parent node.

**Parameters**:
- `node` (TreeNode<TId>): The parent node.

**Returns**: `IQueryable<TreeNode<TId>>`  
A collection of child nodes.

### `GetByIdAsync(TId id)`

Asynchronously retrieves a node by its identifier.

**Parameters**:
- `id` (TId): The identifier of the node.

**Returns**: `Task<TreeNode<TId>?>`  
The task result contains the node if found; otherwise, `null`.

### `GetParentAsync(TreeNode<TId> node)`

Asynchronously retrieves the parent of the specified node.

**Parameters**:
- `node` (TreeNode<TId>): The node whose parent is to be retrieved.

**Returns**: `Task<TreeNode<TId>?>`  
The task result contains the parent node if found; otherwise, `null`.

### `GetParentAsync(TId id)`

Asynchronously retrieves the parent of the node specified by its identifier.

**Parameters**:
- `id` (TId): The identifier of the node.

**Returns**: `Task<TreeNode<TId>?>`  
The task result contains the parent node if found; otherwise, `null`.

### `AttachParentAsync(TId nodeId, TId parentId)`

Asynchronously attaches a node to a new parent.

**Parameters**:
- `nodeId` (TId): The identifier of the node to be attached.
- `parentId` (TId): The identifier of the parent node.

**Returns**: `Task`

### `DetachFromParentAsync(TId nodeId)`

Asynchronously detaches a node from its current parent.

**Parameters**:
- `nodeId` (TId): The identifier of the node to be detached.

**Returns**: `Task`

### `DetachFromTreeAsync(TId nodeId)`

Asynchronously removes a node from the tree and reassigns its children to its parent node.

**Parameters**:
- `nodeId` (TId): The identifier of the node to be removed.

**Returns**: `Task`