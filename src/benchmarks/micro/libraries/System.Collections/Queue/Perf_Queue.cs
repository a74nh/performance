// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks;

namespace System.Collections.Tests
{
    [BenchmarkCategory(Categories.Libraries, Categories.Collections, Categories.GenericCollections)]
    [GenericTypeArguments(typeof(int))]
    [GenericTypeArguments(typeof(string))]
    [GenericTypeArguments(typeof(Guid))]
    public class Perf_queue<TElement>
    {
        [Params(10, 100, 1000)]
        public int Size;

        private TElement[] _items;
        private Queue<TElement> _queue;

        [GlobalSetup]
        public void Setup()
        {
            _items = ValuesGenerator.Array<TElement>(Size);
            _queue = new Queue<TElement>(Size);
        }

        [Benchmark]
        public void Dequeue_And_Enqueue()
        {
            // benchmarks dequeue and enqueue operations
            // for heaps of fixed size.

            var queue = _queue;
            var items = _items;

            // populate the heap: incorporated in the 
            // benchmark to achieve determinism.
            foreach (TElement element in items)
            {
                queue.Enqueue(element);
            }

            foreach (TElement element in items)
            {
                queue.Dequeue();
                queue.Enqueue(element);
            }

            // Drain the heap
            while (queue.Count > 0)
            {
                queue.Dequeue();
            }
        }
    }
}
