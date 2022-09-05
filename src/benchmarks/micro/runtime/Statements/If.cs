// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MicroBenchmarks;
using System.Runtime.CompilerServices;

namespace IfStatements
{
    [BenchmarkCategory(Categories.Runtime)]
    public unsafe class IfStatements
    {
        private const int Iterations = 10000;
        private const int MaxArgsPassed = 4; // Num of args in Consume

        private static int[] inputs;
        private static int[] inputs_sequential;
        private static int[] inputs_zeros;

        private static int s_seed;

        private int[] _array;

        static void InitRand() {
            s_seed = 7774755;
        }

        static int Rand(ref int seed) {
            s_seed = (s_seed * 77 + 13218009) % 3687091;
            return seed;
        }

        public IfStatements()
        {
            _array = new int[300];
            inputs = new int[Iterations + MaxArgsPassed];
            inputs_sequential = new int[Iterations + MaxArgsPassed];
            inputs_zeros = new int[Iterations + MaxArgsPassed];
            for (int i = 0; i < inputs.Length; i++) {
                inputs[i] = Rand(ref s_seed) - 1;
                inputs_sequential[i] = i;
                inputs_zeros[i] = 0;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void Consume(int op1, int op2, int op3, int op4) {
            return;
        }

        /* Array is filled with random data.
           Mod 2 in the first test gives roughly 50/50 split.
           Each following test increases the mod, giving more weight to jumping over the condition.
        */

        [Benchmark]
        public void Single() {
            for (int i = 0; i < Iterations; i++) {
                SingleInner(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleInner(int op1) {
            if (op1 % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void Single3() {
            for (int i = 0; i < Iterations; i++) {
                SingleInner3(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleInner3(int op1) {
            if (op1 % 3 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }


        [Benchmark]
        public void Single4() {
            for (int i = 0; i < Iterations; i++) {
                SingleInner4(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleInner4(int op1) {
            if (op1 % 4 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void Single5() {
            for (int i = 0; i < Iterations; i++) {
                SingleInner5(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleInner5(int op1) {
            if (op1 % 5 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void Single6() {
            for (int i = 0; i < Iterations; i++) {
                SingleInner6(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleInner6(int op1) {
            if (op1 % 6 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        /* Array is filled with random data.
           Mod 2 in the first test gives roughly 50/50 split.
           Each following test increases the mod, giving more weight to executing the condition.
        */

        [Benchmark]
        public void SingleRev() {
            for (int i = 0; i < Iterations; i++) {
                SingleRevInner(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleRevInner(int op1) {
            if (op1 % 2 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleRev3() {
            for (int i = 0; i < Iterations; i++) {
                SingleRevInner3(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleRevInner3(int op1) {
            if (op1 % 3 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }


        [Benchmark]
        public void SingleRev4() {
            for (int i = 0; i < Iterations; i++) {
                SingleRevInner4(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleRevInner4(int op1) {
            if (op1 % 4 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleRev5() {
            for (int i = 0; i < Iterations; i++) {
                SingleRevInner5(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleRevInner5(int op1) {
            if (op1 % 5 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleRev6() {
            for (int i = 0; i < Iterations; i++) {
                SingleRevInner6(inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleRevInner6(int op1) {
            if (op1 % 6 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        /* Array is filled with sequential data.
           Mod 2 in the first test gives exactly a 50/50 split.
           Each following test increases the mod, giving more weight to jumping over the condition.
           This gives a regular pattern of a single pass followed by multiple fail conditions.
        */

        [Benchmark]
        public void SingleSeq() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqInner(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqInner(int op1) {
            if (op1 % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleSeq3() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqInner3(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqInner3(int op1) {
            if (op1 % 3 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }


        [Benchmark]
        public void SingleSeq4() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqInner4(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqInner4(int op1) {
            if (op1 % 4 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleSeq5() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqInner5(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqInner5(int op1) {
            if (op1 % 5 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleSeq6() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqInner6(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqInner6(int op1) {
            if (op1 % 6 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        /* Array is filled with sequential data.
           Mod 2 in the first test gives exactly a 50/50 split.
           Each following test increases the mod, giving more weight to executing the condition.
           This gives a regular pattern of a single fail followed by multiple pass conditions.
        */

        [Benchmark]
        public void SingleSeqRev() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqRevInner(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqRevInner(int op1) {
            if (op1 % 2 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleSeqRev3() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqRevInner3(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqRevInner3(int op1) {
            if (op1 % 3 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }


        [Benchmark]
        public void SingleSeqRev4() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqRevInner4(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqRevInner4(int op1) {
            if (op1 % 4 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleSeqRev5() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqRevInner5(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqRevInner5(int op1) {
            if (op1 % 5 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleSeqRev6() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqRevInner6(inputs_sequential[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqRevInner6(int op1) {
            if (op1 % 6 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        /* Array is filled with zeros.
           Jump is always taken or always not taken
        */

        [Benchmark]
        public void SingleSeqAlways() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqAlwaysInner(inputs_zeros[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqAlwaysInner(int op1) {
            if (op1 % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void SingleSeqAlwaysNever() {
            for (int i = 0; i < Iterations; i++) {
                SingleSeqAlwaysNeverInner(inputs_zeros[i]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleSeqAlwaysNeverInner(int op1) {
            if (op1 % 2 != 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }




        [Benchmark]
        public void MoveNext() {
            int index = 0;
            for (int i = 0; i < Iterations; i++) {
                MoveNextInner(ref index);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void MoveNextInner(ref int index)
        {
            // It is tempting to use the remainder operator here but it is actually much slower
            // than a simple comparison and a rarely taken branch.
            // JIT produces better code than with ternary operator ?:
            int tmp = index + 1;
            if (tmp == _array.Length)
            {
                tmp = 0;
            }
            index = tmp;
        }




        [Benchmark]
        public void And() {
            for (int i = 0; i < Iterations; i++) {
                AndInner(inputs[i], inputs[i+1]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AndInner(int op1, int op2) {
            if (op1 % 2 == 0 && op2 % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, op2, 0, 0);
        }

        [Benchmark]
        public void AndAnd() {
            for (int i = 0; i < Iterations; i++) {
                AndAndInner(inputs[i], inputs[i+1], inputs[i+2]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AndAndInner(int op1, int op2, int op3) {
            if (op1 % 2 == 0 && op2 % 2 == 0 && op3 % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, op2, op3, 0);
        }

        [Benchmark]
        public void AndAndAnd() {
            for (int i = 0; i < Iterations; i++) {
                AndAndAndInner(inputs[i], inputs[i+1], inputs[i+2], inputs[i+3]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AndAndAndInner(int op1, int op2, int op3, int op4) {
            if (op1 % 2 == 0 && op2 % 2 == 0 && op3 % 2 == 0 && op4 % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, op2, op3, op4);
        }

        [Benchmark]
        public void Or() {
            for (int i = 0; i < Iterations; i++) {
                OrInner(inputs[i], inputs[i+1]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void OrInner(int op1, int op2) {
            if (op1 % 2 == 0 || op2 % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, op2, 0, 0);
        }

        [Benchmark]
        public void OrOr() {
            for (int i = 0; i < Iterations; i++) {
                OrOrInner(inputs[i], inputs[i+1], inputs[i+2]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void OrOrInner(int op1, int op2, int op3) {
            if (op1 % 2 == 0 || op2 % 2 == 0 || op3 % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, op2, op3, 0);
        }

        [Benchmark]
        public void AndOr() {
            for (int i = 0; i < Iterations; i++) {
                AndOrInner(inputs[i], inputs[i+1], inputs[i+2]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AndOrInner(int op1, int op2, int op3) {
            if (op1 % 2 == 0 && op2 % 2 == 0 || op3 % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, op2, op3, 0);
        }

        [Benchmark]
        public void SingleArray() {
            for (int i = 0; i < Iterations; i++) {
                SingleArrayInner(i);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SingleArrayInner(int op1) {
            if (inputs[op1] % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, 0, 0, 0);
        }

        [Benchmark]
        public void AndArray() {
            for (int i = 0; i < Iterations; i++) {
                AndArrayInner(i, i+1);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AndArrayInner(int op1, int op2) {
            if (inputs[op1] % 2 == 0 && inputs[op2] % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, op2, 0, 0);
        }

        [Benchmark]
        public void OrArray() {
            for (int i = 0; i < Iterations; i++) {
                OrArrayInner(i, i+1);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void OrArrayInner(int op1, int op2) {
            if (inputs[op1] % 2 == 0 || inputs[op2] % 2 == 0) {
                op1 = 5;
            }
            Consume(op1, op2, 0, 0);
        }
    }
}
