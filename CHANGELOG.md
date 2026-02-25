# Changelog

All notable changes to Morpeh.Queries will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

---

## [2024.1.1] — 2026-02-23

### Added

- **`QueryFixedSystem`** — base class combining `QuerySystem` + `IFixedSystem` for FixedUpdate systems.
- **`QueryLateSystem`** — base class combining `QuerySystem` + `ILateSystem` for LateUpdate systems.
- **`QueryCleanupSystem`** — base class combining `QuerySystem` + `ICleanupSystem` for cleanup systems.
- **`CompiledQuery.Count()`** — returns the number of matching entities via `filter.GetLengthSlow()`.
- **`QueryBuilder.Extend<T>()`** — support for `IFilterExtension` custom filter extensions.
- **`ForFirst` overloads** in `QueryBuilderExtensions` — execute a lambda for the *first* matching entity only. Available in all variants:
  - `ForFirst(Action<Entity>)` — entity only
  - `ForFirst<T1>(Action<Entity, T1>)` / `ForFirst<T1>(Action<T1>)` — 1 component
  - `ForFirst<T1,T2>(...)` — 2 components
  - `ForFirst<T1,T2,T3>(...)` — 3 components
  - `ForFirst<T1,T2,T3,T4>(...)` — 4 components

### Fixed

- **Morpeh 2024.1.1 compatibility — `QueryBuilder.Build()` reflection**: `FilterBuilder.With<T>()` and `Without<T>()` are now zero-parameter instance methods. Fixed invocation to pass `null` instead of `new object[] { filterBuilder }`, preventing `TargetParameterCountException` at runtime.
- **Morpeh 2024.1.1 compatibility — `CompiledQuery` empty filter crash**: In Morpeh 2024.1.1 an empty `Filter` matches *all* archetypes, making direct `world.entities[]` iteration unnecessary and unsafe (null archetype slots). Fixed by always using the `Filter` enumerator regardless of whether include/exclude types are present.
- **Morpeh 2024.1.1 compatibility — `QueryBuilderExtensions.Also()`**: `FilterBuilder` changed from a class to a `struct` in Morpeh 2024.1.1. The previous `Action<FilterBuilder>` signature silently discarded all modifications. Fixed by changing to `Func<FilterBuilder, FilterBuilder>`.
- **`QueryHelper.ValidateRequest`**: `hasProblems` was declared but never set to `true` inside error branches, meaning validation exceptions were never thrown even when components were missing from the filter. Fixed.
- **`QuerySystem` compile error without `MORPEH_BURST`**: `IsUpdatedEveryFrame`, `AddExecutor()`, `CreateQuery()`, and `m_executors` were incorrectly placed inside `#if MORPEH_BURST`, causing compile errors in non-Burst projects. Moved outside the conditional block.
- **`EventListenerExtensions` method name**: `CompiledCompiledEventListener` (duplicate word) renamed to `ForEach`.

### Improved

- **`QueryBuilder.Build()` performance**: `MakeGenericMethod` results are now cached in static `Dictionary<Type, MethodInfo>` dictionaries (`s_withMethodCache`, `s_withoutMethodCache`), eliminating repeated reflection allocations on every `Build()` call.

---

## [2024.1] — initial release

- Initial release of Morpeh.Queries.
- Lambda-based `ForEach` and `ForEachParallel` queries.
- Jobs & Burst support via `ScheduleJob`.
- World Events and Entity Events system.
- `QueryBuilderGlobals` for project-wide filter constraints.
- Automatic query validation (`QueryHelper`).
