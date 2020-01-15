namespace GMacBenchmarks2.Benchmarks.Symbolic
{
    //public class GMacSymbolicBenchmark2
    //{
    //    private readonly GMacRandomGenerator _randGen = new GMacRandomGenerator();

    //    private readonly List<GaSymMultivector> _sparseMultivectors =
    //        new List<GaSymMultivector>();

    //    private readonly List<GaTreeMultivector> _treeMultivectors =
    //        new List<GaTreeMultivector>();

    //    private GaSymMapBilinearHash _sparseOpTable;

    //    private GaSymMapBilinearArray _fullOpTable;

    //    private GaSymProductLookup _opCombination;


    //    //[Params(2, 3, 4, 5, 6, 7, 8, 9, 10, 11)]
    //    [Params(3, 4, 5)]
    //    public int VSpaceDimension { get; set; }

    //    public int GaSpaceDimension 
    //        => GMacMathUtils.GaSpaceDimension(VSpaceDimension);


    //    private void ReportSize(string msg, long size)
    //    {
    //        var sizeInKBytes = size / (double)1024;
    //        Console.Out.Write(msg);
    //        Console.Out.Write(sizeInKBytes.ToString("###,###,###,###.000"));
    //        Console.Out.WriteLine(" KBytes");
    //    }

    //    [GlobalSetup]
    //    public void Setup()
    //    {
    //        _sparseOpTable = GaSymMapBilinearHash.CreateOpTable(VSpaceDimension);
    //        _fullOpTable = GaSymMapBilinearArray.CreateOpTable(VSpaceDimension);

    //        _randGen.ResetGenerator(10);

    //        _sparseMultivectors.Add(
    //            _randGen.GetFullMultivector(GaSpaceDimension)
    //            );

    //        _sparseMultivectors.Add(
    //            _randGen.GetFullMultivector(GaSpaceDimension)
    //            );

    //        _opCombination = 
    //            _sparseMultivectors[0]
    //            .NonZeroBasisBladeIds
    //            .OpCombination(_sparseMultivectors[1].NonZeroBasisBladeIds);

    //        foreach (var mv in _sparseMultivectors)
    //            _treeMultivectors.Add(mv.ToTreeMultivector());

    //        //var mv1 = _multivectors[0].OuterProduct(_multivectors[1]);
    //        //var mv2 = _sparseOpTable.Map(_multivectors[0], _multivectors[1]);
    //        //var mv3 = _fullOpTable.Map(_multivectors[0], _multivectors[1]);

    //        ReportSize("Sparse Multivectors List Size: ", _sparseMultivectors.SizeInBytes());
    //        ReportSize("Tree Multivectors List Size: ", _treeMultivectors.SizeInBytes());
    //        ReportSize("Outer Product Bilinear Combination Table Size: ", _opCombination.SizeInBytes());
    //        ReportSize("Sparse Outer Product Lookup Table Size: ", _sparseOpTable.SizeInBytes());
    //        ReportSize("Full Outer Product Lookup Table Size: ", _fullOpTable.SizeInBytes());
    //    }

    //    [Benchmark]
    //    public GaSymMultivector GacfComputedOuterProduct()
    //    {
    //        //GMacCompilerOptions.ComputeIsNegativeEGp = true;
    //        return _sparseMultivectors[0].Op(_sparseMultivectors[1]);
    //    }

    //    //[Benchmark]
    //    //public GaMultivector GacfLookupOuterProduct()
    //    //{
    //    //    GMacCompilerOptions.ComputeIsNegativeEGp = false;
    //    //    return _sparseMultivectors[0].OuterProduct(_sparseMultivectors[1]);
    //    //}

    //    //[Benchmark]
    //    //public GaTreeMultivector TreeOuterProduct()
    //    //{
    //    //    return _treeMultivectors[0].OuterProduct(_treeMultivectors[1]);
    //    //}

    //    //[Benchmark]
    //    //public GaMultivector CombinationOuterProduct()
    //    //{
    //    //    return _opCombination.Map(_sparseMultivectors[0], _sparseMultivectors[1]);
    //    //}

    //    //[Benchmark]
    //    //public GaMultivector LookupSparseOuterProduct()
    //    //{
    //    //    return _sparseOpTable.Map(_sparseMultivectors[0], _sparseMultivectors[1]);
    //    //}

    //    //[Benchmark]
    //    //public GaMultivector LookupFullOuterProduct()
    //    //{
    //    //    return _sparseOpTable.Map(_sparseMultivectors[0], _sparseMultivectors[1]);
    //    //}
    //}
}