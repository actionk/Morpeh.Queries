# Morpeh.Queries [![Github license](https://img.shields.io/github/license/codewriter-packages/Morpeh.Events.svg?style=flat-square)](#) [![Unity 2020](https://img.shields.io/badge/Unity-2020+-2296F3.svg?style=flat-square)](#) ![GitHub package.json version](https://img.shields.io/github/package-json/v/actionk/Morpeh.Queries?style=flat-square)

Alternative to built-in filters using lambdas for [Morpeh ECS](https://github.com/scellecs/morpeh).

## Table of Contents

- [Example](#example)
- [Comparison & Performance](#comparison--performance)
    - [Before](#before)
    - [After](#after)
- [Usage](#usage)
    - [Creating a query](#creating-a-query)
    - [.WithAll](#withall)
    - [.WithNone](#withnone)
    - [.With<T>](#with-t)
    - [.Without<T>](#without-t)
    - [.Also](#also)
    - [.ForEach](#foreach)
- [Jobs & Burst](#jobs--burst)
    - [.ScheduleJob](#schedulejob)
    - [.ForEachNative](#foreachnative)
- [Options](#options)
    - [Automatic Validation](#automatic-validation)
    - [OnAwake & OnUpdate](#onawake--onupdate)
- [License](#license)

## Example

```csharp
public class ExampleQuerySystem : QuerySystem
{
    protected override void Configure()
    {
        CreateQuery()
            .WithAll<PlayerComponent, ViewComponent, Reference<Transform>>()
            .WithNone<Dead>()
            .ForEach((Entity entity, ref Reference<Transform> transformRerefence, ref ViewComponent viewComponent) =>
            {
                testQueryComponent.value++;
            });
    }
}
```

## Comparison & Performance

### Before

Usually, the regular system in Morpeh is implemented this way:

```csharp
public class NoQueriesTestSystem : UpdateSystem
{
    private Filter filter;

    public override void OnAwake()
    {
        filter = World.Filter.With<TestComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            ref var testQueryComponent = ref entity.GetComponent<TestComponent>();
            testQueryComponent.value++;
        }
    }
}
```

There will be `1 000 000` entities and `100` iterations of testing for this and the other examples;

Results: **14.43** seconds.

In order to optimize this, we can store a reference to the `Stash<T>` that contains all the components of type `TestComponent` for different entities:

```csharp
public class NoQueriesUsingStashTestSystem : UpdateSystem
{
    private Filter filter;
    private Stash<TestComponent> stash;

    public override void OnAwake()
    {
        filter = World.Filter.With<TestComponent>();
        stash = World.GetStash<TestComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            ref var testQueryComponent = ref stash.Get(entity);
            testQueryComponent.value++;
        }
    }
}
```

Results: **9.05** seconds (-38%)

### After

In order to remove the boilerplate for acquiring the components and still have it optimized using Stashes, you can use the Queries from this plugin instead: 

```csharp
public class WithQueriesSystem : QuerySystem
{
    protected override void Configure()
    {
        CreateQuery()
            .With<TestComponent>()
            .ForEach((Entity entity, ref TestComponent testQueryComponent) =>
            {
                testQueryComponent.value++;
            });
    }
}
```

Results: **9.45** seconds (+5%)

As you can see, we're using a `QuerySystem` abstract class that implements the queries inside, therefore we have no `OnUpdate` method anymore. If you need the `deltaTime` though, you can acquire it using `protected float deltaTime` field in `QuerySystem`, which is updated every time `QuerySystem.OnUpdate()` is called.

Performance-wise, it's a bit slower than the optimized solution that we've looked previously (because of using lambdas), but still faster that the "default" one and is **much** smaller than both of them.

## Usage

### Creating a query

You should define all the queries inside `Configure` method.

`CreateQuery()` returns an object of type `QueryConfigurer` that has many overloads for filtering that you can apply before describing the `ForEach` lambda. 

You can also **combine multiple filtering** calls in a sequence before describing the `ForEach` lambda:

```csharp
CreateQuery()
    .WithAll<TestComponent, DamageComponent>
    .WithNone<Dead, Inactive>()
    .ForEach(...)
```

### .WithAll

Selects all the entities that have **all** of the specified components.

```csharp
CreateQuery()
    .WithAll<TestComponent, DamageComponent>()
    .ForEach(...)
CreateQuery()
    .WithAll<TestComponent, DamageComponent, PlayerComponent, ViewComponent>()
    .ForEach(...)
```

Supports up to 8 arguments (but you can extend it if you want).

Equivalents in Morpeh:
```csharp
Filter = Filter.With<TestComponent>().With<DamageComponent>();
Filter = Filter.With<TestComponent>().With<DamageComponent>().With<PlayerComponent>().With<ViewComponent>();
```

### .WithNone

Selects all the entities that have **none** of the specified components.

```csharp
CreateQuery()
    .WithNone<Dead, Inactive>()
    .ForEach(...)
CreateQuery()
    .WithNone<Dead, Inactive, PlayerComponent, ViewComponent>()
    .ForEach(...)
```

Supports up to 8 arguments (but you can extend it if you want).

Equivalents in Morpeh:
```csharp
Filter = Filter.Without<Dead>().Without<Inactive>();
Filter = Filter.Without<Dead>().Without<Inactive>().Without<PlayerComponent>().Without<ViewComponent>();
```

### .With<T>

Equivalent to Morpeh's `Filter.With<T>`.

### .Without<T>

Equivalent to Morpeh's `Filter.Without<T>`.

### .Also

You can specify your custom filter if you want:
```csharp
CreateQuery()
    .WithAll<TestComponent, DamageComponent>()
    .Also(filter => filter.Without<T>())
    .ForEach(...)
```

### .ForEach

There are multiple supported options for describing a lambda:

```csharp
.ForEach<TestComponent>(ref TestComponent component)
.ForEach<TestComponent>(Entity entity, ref TestComponent component)
```

You can either receive the entity as the 1st parameter or you can just skip it if you only need the components.

Supported up to 8 components (you can extend it if you want)

**Restrictions**

* You can only receive components as **ref**
* You can't receive Aspects

## Jobs & Burst

You can also use Unity's Jobs system & Burst to run the calculations in background when executing a query instead of running it on the main thread. Use `ScheduleJob` for that.

### .ScheduleJob

```csharp
[BurstCompile]
public struct TestJobParallelForReference : IEntityQueryJobParallelFor<TestComponent>
{
    public NativeFilter Entities { get; set; }
    public NativeStash<TestComponent> ComponentT1 { get; set; }

    public void Execute(int index)
    {
        var entityId = Entities[index];
        ref var component = ref ComponentT1.Get(entityId, out var exists);
        if (exists)
        {
            component.value++;
        }
    }
}

public class JobsQueriesTestSystem : QuerySystem
{
    protected override void Configure()
    {
        CreateQuery()
            .With<TestComponent>()
            .ScheduleJob<TestJobParallelForReference, TestComponent>();
    }
}
```

Results: ~2.40 seconds (`1 000 000` entities & `100` iterations)

Supports up to `4` arguments (you can extend it if you want).

### .ForEachNative

```csharp
[BurstCompile]
public struct CustomTestJobParallelForReference : IJobParallelFor
{
    [ReadOnly]
    public NativeFilter entities;

    public NativeStash<TestComponent> testComponentStash;

    public void Execute(int index)
    {
        var entityId = entities[index];
        ref var component = ref testComponentStash.Get(entityId, out var exists);
        if (exists)
        {
            component.value++;
        }
    }
}

public class CustomJobsQueriesTestSystem : QuerySystem
{
    protected override void Configure()
    {
        CreateQuery()
            .With<TestComponent>()
            .ForEachNative((NativeFilter entities, NativeStash<TestComponent> testComponentStash) =>
            {
                var parallelJob = new CustomTestJobParallelForReference
                {
                    entities = entities,
                    testComponentStash = testComponentStash
                };
                var parallelJobHandle = parallelJob.Schedule(entities.length, 64);
                parallelJobHandle.Complete();
            });
    }
}
```

Results: ~2.40 seconds (`1 000 000` entities & `100` iterations)

Supports up to `6` arguments (you can extend it if you want).

## Options

### Automatic Validation

Be default, the query engine applies checks when you create a query: all the components that you're using in `ForEach` should also be defined in a query using `With` or `WithAll` to guarantee that the components exist on the entities that the resulting `Filter` returns.

This validation **only happens once** when creating a query so it doesn't affect the performance of your `ForEach` method! 

However, if you're willing to disable the validation for some reason, you can use `.SkipValidation(true)` method:

```csharp
CreateQuery()
    .WithAll<TestComponent, DamageComponent>
    .SkipValidation(true)
    .ForEach(...)
```

### OnAwake & OnUpdate

You can override `OnAwake` & `OnUpdate` methods of `QuerySystem` if you want to:

```charp
public override void OnAwake()
{
    base.OnAwake();
}

public override void OnUpdate(float newDeltaTime)
{
    base.OnUpdate(newDeltaTime);
}
```

Don't forget to call the base method, otherwise `Configure` and/or queries execution won't happen!


## License

Morpeh.Queries is [MIT licensed](./LICENSE.md).