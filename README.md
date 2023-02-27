# Morpeh.Queries [![Github license](https://img.shields.io/github/license/codewriter-packages/Morpeh.Events.svg?style=flat-square)](#) [![Unity 2020](https://img.shields.io/badge/Unity-2020+-2296F3.svg?style=flat-square)](#) ![GitHub package.json version](https://img.shields.io/github/package-json/v/actionk/Morpeh.Queries?style=flat-square)

Alternative to built-in filters using lambdas for [Morpeh ECS](https://github.com/scellecs/morpeh).

* Lambda syntax for querying entities & their Components
* Supporting jobs & burst
  * Automatic jobs scheduling
  * Jobs dependencies

## Table of Contents

- [Examples](#examples)
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
    - [Scheduling Parallel Jobs](#scheduling-paralell-jobs)
    - [Waiting for another job to finish](#waiting-for-another-job-to-finish)
    - [.ForEachNative](#foreachnative)
- [Additions](#additions)
    - [Automatic Validation](#automatic-validation)
    - [OnAwake & OnUpdate](#onawake--onupdate)
- [License](#license)

## Examples

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

```csharp
public class CustomSequentialJobQueriesTestSystem : QuerySystem
{
    protected override void Configure()
    {
        var jobHandle = CreateQuery()
            .With<TestComponent>()
            .ScheduleJob<CustomTestJobParallel>();

        CreateQuery()
            .With<TestComponent>()
            .ScheduleJob<CustomTestJobParallelAfterwards>(jobHandle);
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

### After (using Burst)

In order to optimize it even further, one can use burst jobs. Firstly, let's create a job:

```csharp
[BurstCompile]
public struct CustomTestJobParallel : IJobParallelFor
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
```

Now we should create a system that will run the job. Let's check how it's done using Morpeh:

```csharp
public class NoQueriesUsingStashJobsTestSystem : UpdateSystem
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
        var nativeFilter = filter.AsNative();
        var parallelJob = new CustomTestJobParallel
        {
            entities = nativeFilter,
            testComponentStash = stash.AsNative()
        };
        var parallelJobHandle = parallelJob.Schedule(nativeFilter.length, 64);
        parallelJobHandle.Complete();
    }
}
```

Results: `1.67` seconds (-83%). 

Jobs are much faster, as you can see, but it requires even more preparations. Let's remove this boilerplate by using this plugin:

```csharp
public class CustomJobQueriesTestSystem : QuerySystem
{
    protected override void Configure()
    {
        CreateQuery()
            .With<TestComponent>()
            .ScheduleJob<CustomTestJobParallel>();
    }
}
```

Results: `1.69` seconds (+1%).

This approach uses `Reflections API` to fill in all the required parameters in the job (`NativeFilter` & `NativeStash<T>`), but the code is well optimized and it affect performance very slightly. Supports as many stashes as you want to. 

## Usage

### Creating a query

You should define all the queries inside `Configure` method.

`CreateQuery()` returns an object of type `QueryBuilder` that has many overloads for filtering that you can apply before describing the `ForEach` lambda. 

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

Note: you should define MORPEH_BURST in your project in order to be able to use Burst in Morpeh.

### .ScheduleJob

If you want to use Unity's Jobs with Burst, you can create your job and call `ScheduleJob<YourJobType>` to schedule it. All the fields (`NativeFilter` & `NativeStash<T>`) will be injected automatically!

Example

```csharp
[BurstCompile]
public struct CustomTestJobParallel : IJobParallelFor
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

public class CustomJobQueriesTestSystem : QuerySystem
{
    protected override void Configure()
    {
        CreateQuery()
            .With<TestComponent>()
            .ScheduleJob<CustomTestJobParallel>();
    }
}
```

Results: ~1.6 seconds (`1 000 000` entities & `100` iterations)

Supports as many NativeStash's as you want.

### Scheduling Parallel Jobs

You can schedule multiple jobs in one systems as well:

```csharp
public class CustomParallelJobQueriesTestSystem : QuerySystem
{
    protected override void Configure()
    {
        CreateQuery()
            .With<TestComponent>()
            .ScheduleJob<CustomTestJobParallel>();

        CreateQuery()
            .With<TestComponent>()
            .ScheduleJob<CustomTestJobParallelValue2>();
    }
}
```

This way they will be executed in parallel and the system will wait for both jobs to finish.

### Waiting for another job to finish

You can also force one job to be dependent on another (to only execute when the 1st is finished):

```csharp
public class CustomSequentialJobQueriesTestSystem : QuerySystem
{
    protected override void Configure()
    {
        var jobHandle = CreateQuery()
            .With<TestComponent>()
            .ScheduleJob<CustomTestJobParallel>();

        CreateQuery()
            .With<TestComponent>()
            .ScheduleJob<CustomTestJobParallelValue2>(jobHandle);
    }
}
```

### .ForEachNative

You can also just receive the native filter & stashes if you want to do your custom logic.

```csharp
[BurstCompile]
public struct CustomTestJobParallel : IJobParallelFor
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
                var parallelJob = new CustomTestJobParallel
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

## Additions

### Automatic Validation

Be default, the query engine applies checks when you create a query: all the components that you're using in `ForEach` should also be defined in a query using `With` or `WithAll` to guarantee that the components exist on the entities that the resulting `Filter` returns.

This validation **only happens once** when creating a query so it doesn't affect the performance of your `ForEach` method! 

However, if you're willing to disable the validation for some reason, you can use `.SkipValidation(true)` method:

```csharp
CreateQuery()
    .WithAll<TestComponent, DamageComponent>()
    .SkipValidation(true)
    .ForEach(...)
```

### Globals

If you want to specify that ALL of your queries should only process entities that have component `X` or don't process entities that have component `Y`, you can use globals feature:

```csharp
QueryBuilderGlobals.With<X>();
QueryBuilderGlobals.Without<Y>();
```

Be careful with using globals though - you might have difficult time debugging your systems :)

Make sure you set this before any systems get initialized (once `CreateQuery()` is converted to lambda or job, the filter is not mutable anymore!).

You can also disable globals for specific queries by using `.IgnoreGlobals(true)`:

```csharp
CreateQuery()
    .With<TestComponent>()
    .IgnoreGlobals(true)
    .ForEach((Entity entity, ref TestComponent testQueryComponent) =>
    {
        testQueryComponent.value++;
    });
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