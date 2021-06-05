namespace GMac.Benchmarks.Benchmarks.Symbolic
{
    //public class GMacSymbolicBenchmark4
    //{
    //    //private readonly GMacRandomGenerator _randGen
    //    //    = new GMacRandomGenerator(10);

    //    private GaSymMultivector _mv1;
    //    private GaSymMultivector _mv2;

    //    private GaSymMultivectorHash _sparseMv1;
    //    private GaSymMultivectorHash _sparseMv2;

    //    private GaTreeMultivector _treeMv1;
    //    private GaTreeMultivector _treeMv2;

    //    private GaSymOperatorBilinear _opSparseTable;
    //    private GaSymOperatorBilinear _opFullTable;
    //    private GaSymProductLookup _opCombinations;

    //    private GaSymOperatorBilinear _gpSparseTable;
    //    private GaSymOperatorBilinear _gpFullTable;
    //    private GaSymProductLookup _gpCombinations;

    //    private GaSymOperatorBilinear _spSparseTable;
    //    private GaSymOperatorBilinear _spFullTable;
    //    private GaSymProductLookup _spCombinations;

    //    private GaSymOperatorBilinear _lcpSparseTable;
    //    private GaSymOperatorBilinear _lcpFullTable;
    //    private GaSymProductLookup _lcpCombinations;

    //    private GaSymFrameEuclidean _frame;


    //    [Params(3, 4, 5, 6, 7, 8, 9, 10)]
    //    //[Params(4)]
    //    public int VSpaceDimension { get; set; }

    //    //[Params(true)]
    //    public bool DisableSymbolicConversion { get; set; } = true;

    //    public int GaSpaceDimension
    //        => GMacMathUtils.GaSpaceDimension(VSpaceDimension);



    //    private void VerifyProductResults(string msg, GaSymMultivectorHash result, GaSymMultivectorHash referenceResult)
    //    {
    //        var diffResult = referenceResult - result;

    //        if (diffResult.IsZero()) return;

    //        Console.Out.WriteLine(msg);

    //        Console.Out.WriteLine("   Result: " + result);
    //        Console.Out.WriteLine();

    //        Console.Out.WriteLine("   Reference: " + referenceResult);
    //        Console.Out.WriteLine();

    //        Console.Out.WriteLine("   Diff: " + diffResult);
    //        Console.Out.WriteLine();
    //    }

    //    private void VerifyProducts()
    //    {
    //        var referenceResult = SparseOp();

    //        VerifyProductResults(
    //            "Outer Product - Tree: ",
    //            TreeMvOp().ToHashMultivector(),
    //            referenceResult
    //            );

    //        VerifyProductResults(
    //            "Outer Product - Sparse Table: ",
    //            LookupSparseOp(),
    //            referenceResult
    //        );

    //        VerifyProductResults(
    //            "Outer Product - Full Table: ",
    //            LookupFullOp(),
    //            referenceResult
    //        );

    //        VerifyProductResults(
    //            "Outer Product - Combinations: ",
    //            CombinationsOp(),
    //            referenceResult
    //        );


    //        referenceResult = SparseMvGp();

    //        VerifyProductResults(
    //            "Geometric Product - Tree: ",
    //            TreeMvGp().ToHashMultivector(),
    //            referenceResult
    //        );

    //        VerifyProductResults(
    //            "Geeometric Product - Sparse Table: ",
    //            LookupSparseGp(),
    //            referenceResult
    //        );

    //        VerifyProductResults(
    //            "Geometric Product - Full Table: ",
    //            LookupFullGp(),
    //            referenceResult
    //        );

    //        VerifyProductResults(
    //            "Geometric Product - Combinations: ",
    //            CombinationsGp(),
    //            referenceResult
    //        );


    //        referenceResult = SparseMvSp();

    //        VerifyProductResults(
    //            "Scalar Product - Sparse Table: ",
    //            LookupSparseSp(),
    //            referenceResult
    //        );

    //        VerifyProductResults(
    //            "Scalar Product - Full Table: ",
    //            LookupFullSp(),
    //            referenceResult
    //        );

    //        VerifyProductResults(
    //            "Scalar Product - Combinations: ",
    //            CombinationsSp(),
    //            referenceResult
    //        );


    //        referenceResult = SparseMvLcp();

    //        VerifyProductResults(
    //            "Left Contraction Product - Sparse Table: ",
    //            LookupSparseLcp(),
    //            referenceResult
    //        );

    //        VerifyProductResults(
    //            "Left Contraction Product - Full Table: ",
    //            LookupFullLcp(),
    //            referenceResult
    //        );

    //        VerifyProductResults(
    //            "Left Contraction Product - Combinations: ",
    //            CombinationsLcp(),
    //            referenceResult
    //        );
    //    }

    //    [GlobalSetup]
    //    public void Setup()
    //    {
    //        GaSymMultivectorTemp.DisableSymbolicConversion = false;

    //        _frame = GaSymFrame.CreateEuclidean(VSpaceDimension);

    //        _sparseMv1 = GaSymMultivectorHash.CreateSymbolicKVector(GaSpaceDimension, "A", _frame.VSpaceDimension / 2);
    //        _sparseMv2 = GaSymMultivectorHash.CreateSymbolicKVector(GaSpaceDimension, "B", _frame.VSpaceDimension / 2);

    //        _treeMv1 = _sparseMv1.ToTreeMultivector();
    //        _treeMv2 = _sparseMv2.ToTreeMultivector();

    //        _mv1 = _sparseMv1.ToMultivector();
    //        _mv2 = _sparseMv2.ToMultivector();

    //        _opSparseTable = GaSymMapBilinearHash.CreateOpTable(_frame);
    //        _opFullTable = GaSymMapBilinearArray.CreateOpTable(_frame);
    //        _opCombinations = _frame.OpCombination();

    //        _gpSparseTable = GaSymMapBilinearHash.CreateGpTable(_frame);
    //        _gpFullTable = GaSymMapBilinearArray.CreateGpTable(_frame);
    //        _gpCombinations = _frame.GpCombination();

    //        _spSparseTable = GaSymMapBilinearHash.CreateSpTable(_frame);
    //        _spFullTable = GaSymMapBilinearArray.CreateSpTable(_frame);
    //        _spCombinations = _frame.SpCombination();

    //        _lcpSparseTable = GaSymMapBilinearHash.CreateLcpTable(_frame);
    //        _lcpFullTable = GaSymMapBilinearArray.CreateLcpTable(_frame);
    //        _lcpCombinations = _frame.LcpCombination();

    //        Console.Out.WriteLine("Space Size: " + VSpaceDimension);
    //        Console.Out.WriteLine();


    //        GaSymMultivectorTemp.DisableSymbolicConversion = DisableSymbolicConversion;
    //    }

    //    #region Outer Product
    //    [Benchmark]
    //    public GaSymMultivector MvOp()
    //    {
    //        return _frame.Op[_mv1, _mv2];
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash SparseOp()
    //    {
    //        return _frame.Op[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaTreeMultivector TreeMvOp()
    //    {
    //        return _treeMv1.Op(_treeMv2);
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash LookupSparseOp()
    //    {
    //        return _opSparseTable[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash LookupFullOp()
    //    {
    //        return _opFullTable[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash CombinationsOp()
    //    {
    //        return _opCombinations.Map(_sparseMv1, _sparseMv2);
    //    }
    //    #endregion

    //    #region Geometric Product
    //    [Benchmark]
    //    public GaSymMultivector MvGp()
    //    {
    //        return _frame.Gp[_mv1, _mv2];
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash SparseMvGp()
    //    {
    //        return _frame.Gp[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaTreeMultivector TreeMvGp()
    //    {
    //        return _frame.Gp(_treeMv1, _treeMv2);
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash LookupSparseGp()
    //    {
    //        return _gpSparseTable[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash LookupFullGp()
    //    {
    //        return _gpFullTable[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash CombinationsGp()
    //    {
    //        return _frame.Map(_sparseMv1, _sparseMv2, _gpCombinations);
    //    }
    //    #endregion

    //    #region Scalar Product
    //    [Benchmark]
    //    public GaSymMultivector MvSp()
    //    {
    //        return _frame.Sp[_mv1, _mv2];
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash SparseMvSp()
    //    {
    //        return _frame.Sp[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash LookupFullSp()
    //    {
    //        return _spFullTable[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash LookupSparseSp()
    //    {
    //        return _spSparseTable[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash CombinationsSp()
    //    {
    //        return _frame.Map(_sparseMv1, _sparseMv2, _spCombinations);
    //    }
    //    #endregion

    //    #region Left Contraction Product
    //    [Benchmark]
    //    public GaSymMultivector MvLcp()
    //    {
    //        return _frame.Lcp[_mv1, _mv2];
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash SparseMvLcp()
    //    {
    //        return _frame.Lcp[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash LookupFullLcp()
    //    {
    //        return _lcpFullTable[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash LookupSparseLcp()
    //    {
    //        return _lcpSparseTable[
    //            _sparseMv1.ToTempMultivector(),
    //            _sparseMv2.ToTempMultivector()
    //        ].ToHashMultivector();
    //    }

    //    [Benchmark]
    //    public GaSymMultivectorHash CombinationsLcp()
    //    {
    //        return _frame.Map(_sparseMv1, _sparseMv2, _lcpCombinations);
    //    }
    //    #endregion
    //}
}
