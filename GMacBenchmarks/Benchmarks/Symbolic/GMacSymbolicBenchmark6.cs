using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using GMac.GMacMath;
using GMac.GMacMath.Symbolic.Metrics;
using UtilLib;

namespace GMacBenchmarks.Benchmarks.Symbolic
{
    public class GMacSymbolicBenchmark6
    {
        private readonly GMacRandomGenerator _rand = new GMacRandomGenerator(10);

        public int Size { get; } = 2048;

        public int[] IntArray { get; private set; }

        public bool[] BoolArray { get; private set; }

        public BitArray BitArray { get; private set; }

        public GaSymMetricOrthonormal BinaryTree { get; private set; }

        public List<int> IndexList { get; private set; }

        public List<int> ResultsList { get; private set; }




        [GlobalSetup]
        public void Setup()
        {
            IntArray = new int[Size];
            BoolArray = new bool[Size];
            BitArray = new BitArray(Size);
            BinaryTree = GaSymMetricOrthonormal.Create(new []{1,-1,-1,1,1,1,1,1,1,-1,-1});
            ResultsList = new List<int>(Size);

            for (var i = 0; i < Size; i++)
            {
                var value = _rand.GetScalar() > 0.5;

                IntArray[i] = value ? -1 : 1;
                BoolArray[i] = value;
                BitArray[i] = value;
                //BinaryTree[i] = value ? -1 : 1;
            }

            IndexList = _rand.GetRangePermutation(Size - 1).ToList();

            //Console.Out.WriteLine(IndexList.Concatenate(", "));

            Console.Out.WriteLine("Int Array Size: " + IntArray.SizeInBytes());
            Console.Out.WriteLine("Bool Array Size: " + BoolArray.SizeInBytes());
            Console.Out.WriteLine("Bit Array Size: " + BitArray.SizeInBytes());
            Console.Out.WriteLine("Binary Tree Size: " + BinaryTree.SizeInBytes());
        }

        [Benchmark]
        public void IntArrayAccess()
        {
            ResultsList.Clear();

            foreach (var i in IndexList)
                ResultsList.Add(IntArray[i]);
        }

        [Benchmark]
        public void BoolArrayAccess()
        {
            ResultsList.Clear();

            foreach (var i in IndexList)
                ResultsList.Add(BoolArray[i] ? -1 : 1);
        }

        [Benchmark]
        public void BitArrayAccess()
        {
            ResultsList.Clear();

            foreach (var i in IndexList)
                ResultsList.Add(BitArray[i] ? -1 : 1);
        }

        [Benchmark]
        public void BinaryTreeAccess()
        {
            ResultsList.Clear();

            foreach (var i in IndexList)
                ResultsList.Add(BinaryTree[i]);
        }
    }
}
