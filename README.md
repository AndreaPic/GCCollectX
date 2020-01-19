# GCCollectX

## Garbage Collector and Memory Fragmentation
In .net sometimes we can have memory allocation problems.

If you allocate too many objects you can fragment heap memory.
Though you have sufficient free memory the system could not be able to find a block of memory to use for allocation request.
In this case you obtain "System.OutOfMemoryException" though there is still available memory.

This scenario can occur when you have WPF\WinForm application or when you need to load a large object when memory is fragmented due to loading and unloading objects.

If you have those problems this library is for you.
In your application you can call:
```C#
GCCollectX.GCX.CollectX();
```
or if you add "using":
```C#
using GCCollectX;
...
...
  GCX.CollectX();
```

With this method, Garbage collector is called (just in case) and are called Windows APIs to "compact a specified heap by coalescing adjacent free blocks of memory and decommitting large free blocks of memory"
The main API called are:
1. SetProcessWorkingSetSize
2. HeapCompact

With first API we ensure that the working size in memory allocation is set to the minimum.
With second API we compact all allocated block of memory.

First we say to the system that we want the minimum page size when one object is allocated to the heap, second we compact all the allocated memory's pages.

## NuGet
You can download package from [this link](https://www.nuget.org/packages/GCCollectX/)
