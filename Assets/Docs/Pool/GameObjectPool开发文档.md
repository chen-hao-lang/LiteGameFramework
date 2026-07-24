# GameObjectPool 设计文档

## 背景
- 回收场景中需要频繁实例化的预制件，避免重复 Instantiate/Destroy 带来的 GC 和性能开销

## 核心决策
### 1. 为什么需要设计 3 个字典字段？
- **objectPools**：prefab → 实例列表。通过 prefab 快速找到对应的实例池，取用或回收实例
- **cloneMap**：实例 → prefab（反向映射）。Release 时只知道实例本身，通过它 O(1) 反查所属预制体，无需遍历所有池
- **parentContainer**：prefab → 父节点。回收的实例统一挂在该父节点下，保持 Hierarchy 整洁

### 2. 为什么用 List 尾部取/删（栈式）而非 Queue？
- RemoveAt(末尾) 是 O(1)，且 LIFO 让最近回收的物体优先复用，CPU 缓存命中率更高

## 工作流程
### Get（获取实例）
```mermaid
flowchart TD
    A[调用 Get] --> B{池中有该 prefab?}
    B -->|否| C[CreatePool 初始化池和父容器]
    B -->|是| D[取出 pool 列表]
    C --> D
    D --> E{池中有空闲实例?}
    E -->|是| F[取列表末尾实例, 激活, 脱离父节点]
    E -->|否| G[Instantiate 新实例, 注册到 cloneMap]
    F --> H[返回实例]
    G --> H
```

### Release（回收实例）
```mermaid
flowchart TD
    A[调用 Release] --> B{cloneMap 能查到该实例?}
    B -->|否| C[LogError: 该实例不属于池]
    B -->|是| D[反查 prefab 和 parentContainer]
    D --> E{父容器存在?}
    E -->|否| F[LogError: 找不到父容器]
    E -->|是| G[归位到父节点, 重置 Transform, 禁用物体]
    G --> H[加入 objectPools 列表]
```

### Clear（清空池）
```mermaid
flowchart TD
    A[调用 Clear] --> B{cloneMap 能查到?}
    B -->|否| C[LogError]
    B -->|是| D[Destroy 池中所有实例, 移除 objectPools 条目]
    D --> E[清理 cloneMap 中所有关联条目]
    E --> F[Destroy 父容器, 移除 parentContainer 条目]
```

## 已知限制
- 不支持自动扩容/缩容，池大小无限增长
- 场景切换时不自动清理，需手动调 Clear()
- 大量预热 Instantiate 会卡主线程