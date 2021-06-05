namespace GMac.Benchmarks.Benchmarks.Numeric.Outermorphisms
{
    ///// <summary>
    ///// Benchmark vector-kvector outer product methods
    ///// </summary>
    //public class Benchmark2
    //{
    //    private GaRandomGenerator _randGen;

    //    private GaNumKVector[] _vectorsArray;
    //    private GaNumKVector[] _kVectorsArray;

    //    [Params(3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
    //    public int VSpaceDim { get; set; }

    //    public int GaSpaceDim
    //        => VSpaceDim.ToGaSpaceDimension();


    //    [GlobalSetup]
    //    public void Setup()
    //    {
    //        _randGen = new GaRandomGenerator(10);

    //        _vectorsArray = new GaNumKVector[VSpaceDim];
    //        _kVectorsArray = new GaNumKVector[VSpaceDim];
    //        for (var i = 0; i < VSpaceDim; i++)
    //        {
    //            _vectorsArray[i] = GaNumKVector.Create(
    //                GaSpaceDim,
    //                1,
    //                _randGen.GetScalars(VSpaceDim, -10, 10)
    //            );
    //        }

    //        GaNumVectorKVectorOpUtils.SetActiveVSpaceDimension(VSpaceDim);
    //    }


    //    [Benchmark]
    //    public GaNumKVector[] OpComputed()
    //    {
    //        _kVectorsArray[0] = _vectorsArray[0];

    //        for (var i = 1; i < VSpaceDim; i++)
    //            _kVectorsArray[i] = _vectorsArray[i].ComputeVectorKVectorOp(_kVectorsArray[i - 1]);

    //        return _kVectorsArray;
    //    }

    //    [Benchmark]
    //    public GaNumKVector[] OpGenerated()
    //    {
    //        _kVectorsArray[0] = _vectorsArray[0];

    //        for (var i = 1; i < VSpaceDim; i++)
    //            _kVectorsArray[i] = _vectorsArray[i].VectorKVectorOp(_kVectorsArray[i - 1]);

    //        return _kVectorsArray;
    //    }

    //    [Benchmark]
    //    public GaNumKVector[] OpGeneratedFunctionTables()
    //    {
    //        _kVectorsArray[0] = _vectorsArray[0];

    //        for (var i = 1; i < VSpaceDim; i++)
    //            _kVectorsArray[i] = _vectorsArray[i].VectorKVectorOp2(_kVectorsArray[i - 1]);

    //        return _kVectorsArray;
    //    }

    //    [Benchmark]
    //    public GaNumKVector[] OpGeneratedIndexTables()
    //    {
    //        _kVectorsArray[0] = _vectorsArray[0];

    //        for (var i = 1; i < VSpaceDim; i++)
    //            _kVectorsArray[i] = _vectorsArray[i].VectorKVectorOp1(_kVectorsArray[i - 1]);

    //        return _kVectorsArray;
    //    }

    //    private static double GetKVectorDifference(GaNumKVector kVector1, GaNumKVector kVector2)
    //    {
    //        var kVectorArraySize = kVector1.ScalarValuesArray.Length;

    //        var d = 0.0d;
    //        for (var i = 0; i < kVectorArraySize; i++)
    //            d += Math.Pow(kVector1[i] - kVector2[i], 2);

    //        return d;
    //    }

    //    public string Validate()
    //    {
    //        var composer = new LinearTextComposer();

    //        var opComputedResult = OpComputed();
    //        var opGeneratedResult = OpGeneratedFunctionTables();
    //        var opGeneratedLookupResult = OpGeneratedIndexTables();

    //        var kVectorsCount = opComputedResult.Length;

    //        for (var i = 0; i < kVectorsCount; i++)
    //        {
    //            var opComputedKVector = opComputedResult[i];
    //            var opGeneratedKVector = opGeneratedResult[i];
    //            var opGeneratedLookupKVector = opGeneratedLookupResult[i];

    //            var diffTriplet = new Triplet<double>(
    //                GetKVectorDifference(opComputedKVector, opGeneratedKVector),
    //                GetKVectorDifference(opGeneratedKVector, opGeneratedLookupKVector),
    //                GetKVectorDifference(opGeneratedLookupKVector, opComputedKVector)
    //            );

    //            if (!diffTriplet.Item1.IsNearZero())
    //            {
    //                composer
    //                    .AppendAtNewLine("Computed vs Generated mismatch: ")
    //                    .AppendLine($"n = {opComputedKVector.VSpaceDimension}, k = {opComputedKVector.Grade}, diff = {diffTriplet.Item1}");
    //            }

    //            if (!diffTriplet.Item2.IsNearZero())
    //            {
    //                composer
    //                    .AppendAtNewLine("Generated vs GeneratedLookup mismatch: ")
    //                    .AppendLine($"n = {opComputedKVector.VSpaceDimension}, k = {opComputedKVector.Grade}, diff = {diffTriplet.Item2}");
    //            }

    //            if (!diffTriplet.Item2.IsNearZero())
    //            {
    //                composer
    //                    .AppendAtNewLine("GeneratedLookup vs Computed mismatch: ")
    //                    .AppendLine($"n = {opComputedKVector.VSpaceDimension}, k = {opComputedKVector.Grade}, diff = {diffTriplet.Item3}");
    //            }
    //        }

    //        return composer.ToString();
    //    }
    //}
}