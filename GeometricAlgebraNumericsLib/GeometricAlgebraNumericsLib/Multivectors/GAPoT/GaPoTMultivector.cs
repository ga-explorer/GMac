namespace GeometricAlgebraNumericsLib.Multivectors.GAPoT
{
    //public sealed class GaPoTMultivector : IGaMultivectorSource<GaNumSarMultivector>
    //{
    //    public IReadOnlyDictionary<int, GaNumSarMultivector> ScalarValuesDictionary { get; }

    //    public int ScalarVSpaceDimension { get; }

    //    public int VSpaceDimension 
    //        => 16;

    //    public int GaSpaceDimension 
    //        => VSpaceDimension.ToGaSpaceDimension();

    //    public int StoredTermsCount 
    //        => ScalarValuesDictionary.Count;

    //    public GaNumSarMultivector this[int id]
    //        => ScalarValuesDictionary.TryGetValue(id, out var scalarValue) 
    //            ? scalarValue : GaNumSarMultivector.CreateZero(ScalarVSpaceDimension);

    //    public GaNumSarMultivector this[int grade, int index] 
    //        => throw new NotImplementedException();


    //    internal GaPoTMultivector(int vSpaceDim, int scalarVSpaceDim, IReadOnlyDictionary<int, GaNumSarMultivector> scalarValuesDictionary)
    //    {
    //        Debug.Assert(
    //            scalarVSpaceDim.IsValidVSpaceDimension() &&
    //            vSpaceDim.IsValidVSpaceDimension() &&
    //            scalarValuesDictionary.All(p =>
    //                p.Key >= 0 && p.Key < GaSpaceDimension
    //            )
    //        );

    //        ScalarVSpaceDimension = scalarVSpaceDim;
    //        //VSpaceDimension = vSpaceDim;
    //        ScalarValuesDictionary = scalarValuesDictionary;
    //    }


    //    public bool TryGetValue(int id, out GaNumSarMultivector value)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool TryGetValue(int grade, int index, out GaNumSarMultivector value)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<int> GetStoredTermIds()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<int> GetNonZeroTermIds()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<GaNumSarMultivector> GetStoredTermScalars()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<GaNumSarMultivector> GetNonZeroTermScalars()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool IsEmpty()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool ContainsStoredTerm(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool ContainsStoredTerm(int grade, int index)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool ContainsStoredKVector(int grade)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool TryGetTerm(int id, out GaTerm<GaNumSarMultivector> term)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool TryGetTerm(int grade, int index, out GaTerm<GaNumSarMultivector> term)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<GaTerm<GaNumSarMultivector>> GetStoredTerms()
    //    {
    //        return ScalarValuesDictionary
    //            .Select(pair => new GaTerm<GaNumSarMultivector>(pair.Key, pair.Value));
    //    }

    //    public IEnumerable<GaTerm<GaNumSarMultivector>> GetStoredTerms(int grade)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<GaTerm<GaNumSarMultivector>> GetNonZeroTerms()
    //    {
    //        return ScalarValuesDictionary
    //            .Where(pair => !pair.Value.IsZero())
    //            .Select(pair => new GaTerm<GaNumSarMultivector>(pair.Key, pair.Value));
    //    }

    //    public IEnumerable<GaTerm<GaNumSarMultivector>> GetNonZeroTerms(int grade)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public GaPoTMultivector GetReverse()
    //    {
    //        var scalarsDictionary = new Dictionary<int, GaNumSarMultivector>();

    //        foreach (var term in GetNonZeroTerms())
    //        {
    //            var value = 
    //                term.BasisBladeId.BasisBladeIdHasNegativeReverse()
    //                    ? -term.ScalarValue
    //                    : term.ScalarValue;

    //            if (scalarsDictionary.ContainsKey(term.BasisBladeId))
    //                scalarsDictionary[term.BasisBladeId] += value;
    //            else
    //                scalarsDictionary.Add(term.BasisBladeId, value);
    //        }

    //        return new GaPoTMultivector(
    //            VSpaceDimension,
    //            ScalarVSpaceDimension,
    //            scalarsDictionary
    //        );
    //    }

    //}
}
