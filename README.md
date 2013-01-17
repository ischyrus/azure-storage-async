azure-storage-async
===================

Aims:

- Support all storage operations using modern async interfaces. This means:
 - Async operations returning a single value of type `T` return a `Task<T>`
 - Async operations that potentially return an unbounded numbre of values of type `T` return an `Observable<T>`
- All long-running operations should support cancellation via cancellation tokens.
