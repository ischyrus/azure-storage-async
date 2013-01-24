azure-storage-async
===================

Aims:

- Support all storage operations using modern async interfaces. This means:
 - Async operations returning a single value of type `T` return a `Task<T>`
 - Async operations that potentially return an unbounded number of values of type `T` return an `Observable<T>`. (Some operations that return a fixed number of values will return a `Task<IList<T>>` or similar.)
- All long-running operations should support cancellation via cancellation tokens.
 - Note that operations returning `Observable<T>` support cancellation in the manner idiomatic for that type (i.e. by calling `Dispose()` on the observable)
