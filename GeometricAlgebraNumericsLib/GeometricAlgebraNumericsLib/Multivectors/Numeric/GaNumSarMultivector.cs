using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Collections;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Structures.BinaryTraversal;
using GeometricAlgebraNumericsLib.Structures.Collections;
using GeometricAlgebraStructuresLib.Frames;
using MathNet.Numerics.LinearAlgebra.Double;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraNumericsLib.Multivectors.Numeric
{
    /// <summary>
    /// Mutable Sparse Array Representation Multivector. Internally, a sparse array (a dictionary) is used to store non-zero terms
    /// </summary>
    public sealed class GaNumSarMultivector : GaNumMultivector
    {
        /// <summary>
        /// Create a zero multivector
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateZero(int vSpaceDim)
        {
            var scalarValues = new Dictionary<int, double>();

            return new GaNumSarMultivector(vSpaceDim, scalarValues);
        }

        /// <summary>
        /// Create a single term multivector
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="id"></param>
        /// <param name="scalarValue"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateTerm(int vSpaceDim, int id, double scalarValue)
        {
            var scalarValues = new Dictionary<int, double>
            {
                {id, scalarValue}
            };

            return new GaNumSarMultivector(vSpaceDim, scalarValues);
        }

        /// <summary>
        /// Create a basis vector multivector
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateBasisVector(int vSpaceDim, int index)
        {
            var scalarValues = new Dictionary<int, double>
            {
                {1 << index, 1.0d}
            };

            return new GaNumSarMultivector(vSpaceDim, scalarValues);
        }

        /// <summary>
        /// Create a basis blade multivector
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateBasisBlade(int vSpaceDim, int id)
        {
            var scalarValues = new Dictionary<int, double>
            {
                {id, 1.0d}
            };

            return new GaNumSarMultivector(vSpaceDim, scalarValues);
        }

        public static GaNumSarMultivector CreateBasisBlade(int vSpaceDim, int grade, int index)
        {
            var scalarValues = new Dictionary<int, double>
            {
                {GaFrameUtils.BasisBladeId(grade, index), 1.0d}
            };

            return new GaNumSarMultivector(vSpaceDim, scalarValues);
        }

        /// <summary>
        /// Create a scalar multivector
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="scalarValue"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateScalar(int vSpaceDim, double scalarValue)
        {
            var scalarValues = new Dictionary<int, double>
            {
                {0, scalarValue}
            };

            return new GaNumSarMultivector(vSpaceDim, scalarValues);
        }

        /// <summary>
        /// Create a unit scalar multivector
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateUnitScalar(int vSpaceDim)
        {
            var scalarValues = new Dictionary<int, double>
            {
                {0, 1.0d}
            };

            return new GaNumSarMultivector(vSpaceDim, scalarValues);
        }

        /// <summary>
        /// Create a pseudoscalar multivector
        /// </summary>
        /// <param name="vSpaceDim"></param>
        /// <param name="scalarValue"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreatePseudoscalar(int vSpaceDim, double scalarValue)
        {
            var scalarValues = new Dictionary<int, double>
            {
                {vSpaceDim.ToGaSpaceDimension() - 1, scalarValue}
            };

            return new GaNumSarMultivector(vSpaceDim, scalarValues);
        }
        
        /// <summary>
        /// Create a multivector from a column vector. The column vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateFromColumn(Matrix matrix, int col)
        {
            Debug.Assert(matrix.RowCount.IsValidGaSpaceDimension());

            var mv = new GaNumSarMultivectorFactory(matrix.RowCount.ToVSpaceDimension());

            for (var row = 0; row < matrix.RowCount; row++)
            {
                var scalarValue = matrix[row, col];

                if (scalarValue != 0.0d)
                    mv.AddTerm(row, scalarValue);
            }

            return mv.GetSarMultivector();
        }

        /// <summary>
        /// Create a multivector from a column vector. The column vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateFromRow(Matrix matrix, int row)
        {
            Debug.Assert(matrix.ColumnCount.IsValidGaSpaceDimension());

            var mv = new GaNumSarMultivectorFactory(matrix.ColumnCount.ToVSpaceDimension());

            for (var col = 0; col < matrix.ColumnCount; col++)
            {
                var scalarValue = matrix[row, col];

                if (scalarValue != 0.0d)
                    mv.AddTerm(row, scalarValue);
            }

            return mv.GetSarMultivector();
        }

        /// <summary>
        /// Create a multivector from a column vector. The column vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateFromColumn(double[,] matrix, int col)
        {
            var rows = matrix.GetLength(0);

            Debug.Assert(rows.IsValidGaSpaceDimension());

            var mv = new GaNumSarMultivectorFactory(rows.ToVSpaceDimension());

            for (var row = 0; row < rows; row++)
            {
                var scalarValue = matrix[row, col];

                if (scalarValue != 0.0d)
                    mv.AddTerm(row, matrix[row, col]);
            }

            return mv.GetSarMultivector();
        }

        /// <summary>
        /// Create a multivector from a row vector. The row vector length
        /// defines the GA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateFromRow(double[,] matrix, int row)
        {
            var cols = matrix.GetLength(1);

            Debug.Assert(cols.IsValidGaSpaceDimension());

            var mv = new GaNumSarMultivectorFactory(cols.ToVSpaceDimension());

            for (var col = 0; col < cols; col++)
            {
                var scalarValue = matrix[row, col];

                if (scalarValue != 0.0d)
                    mv.AddTerm(row, matrix[row, col]);
            }

            return mv.GetSarMultivector();
        }

        /// <summary>
        /// Create a vector multivector from a column vector. The column vector
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateVectorFromColumn(Matrix matrix, int col)
        {
            var vSpaceDim = matrix.RowCount;

            var mv = new GaNumSarMultivectorFactory(vSpaceDim);

            for (var row = 0; row < matrix.RowCount; row++)
                mv.AddTerm(1, row, matrix[row, col]);

            return mv.GetSarMultivector();
        }

        /// <summary>
        /// Create a vector multivector from a row vector. The row vector length
        /// defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateVectorFromRow(Matrix matrix, int row)
        {
            var vSpaceDim = matrix.ColumnCount;

            var mv = new GaNumSarMultivectorFactory(vSpaceDim);

            for (var col = 0; col < matrix.ColumnCount; col++)
                mv.AddTerm(1, col, matrix[row, col]);

            return mv.GetSarMultivector();
        }

        /// <summary>
        /// Create a vector multivector from a column vector. The column vector
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateVectorFromColumn(double[,] exprArray, int col)
        {
            var rowsCount = exprArray.GetLength(0);
            var vSpaceDim = rowsCount;

            var mv = new GaNumSarMultivectorFactory(vSpaceDim);

            for (var row = 0; row < rowsCount; row++)
                mv.AddTerm(1, row, exprArray[row, col]);

            return mv.GetSarMultivector();
        }

        /// <summary>
        /// Create a vector multivector from a row vector. The row vector
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="exprArray"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateVectorFromRow(double[,] exprArray, int row)
        {
            var colsCount = exprArray.GetLength(1);
            var vSpaceDim = colsCount;

            var mv = new GaNumSarMultivectorFactory(vSpaceDim);

            for (var col = 0; col < colsCount; col++)
                mv.AddTerm(1, col, exprArray[row, col]);

            return mv.GetSarMultivector();
        }

        /// <summary>
        /// Create a vector multivector from an array of scalars. The array
        /// length defines the VA space dimension of the multivector
        /// </summary>
        /// <param name="scalars"></param>
        /// <returns></returns>
        public static GaNumSarMultivector CreateVectorFromScalars(params double[] scalars)
        {
            var vSpaceDim = scalars.Length;

            var mv = new GaNumSarMultivectorFactory(vSpaceDim);

            for (var index = 0; index < scalars.Length; index++)
                mv.AddTerm(1, index, scalars[index]);

            return mv.GetSarMultivector();
        }


        public static GaNumSarMultivector operator -(GaNumSarMultivector mv)
        {
            return mv.GetNonZeroTerms().GaNegative().CreateSarMultivector(mv.VSpaceDimension);
        }

        public static GaNumSarMultivector operator +(GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            return mv1.GetAdditionTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector operator -(GaNumSarMultivector mv1, GaNumSarMultivector mv2)
        {
            return mv1.GetSubtractionTerms(mv2).SumAsSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector operator *(GaNumSarMultivector mv1, double s)
        {
            return mv1.GetScaledTerms(s).CreateSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector operator *(double s, GaNumSarMultivector mv1)
        {
            return mv1.GetScaledTerms(s).CreateSarMultivector(mv1.VSpaceDimension);
        }

        public static GaNumSarMultivector operator /(GaNumSarMultivector mv1, double s)
        {
            return mv1.GetScaledTerms(1.0d / s).CreateSarMultivector(mv1.VSpaceDimension);
        }


        public IReadOnlyDictionary<int, double> ScalarValuesDictionary { get; }

        public override double this[int grade, int index] 
            => ScalarValuesDictionary.TryGetValue(GaFrameUtils.BasisBladeId(grade, index), out var scalarValue) 
                ? scalarValue : 0.0d;

        public override double this[int id] =>
            ScalarValuesDictionary.TryGetValue(id, out var scalarValue) 
                ? scalarValue : 0.0d;

        public override int StoredTermsCount 
            => ScalarValuesDictionary.Count;


        internal GaNumSarMultivector(int vSpaceDim, IReadOnlyDictionary<int, double> scalarValuesDictionary)
            : base(vSpaceDim)
        {
            Debug.Assert(
                scalarValuesDictionary.All(p => 
                    p.Key >= 0 && p.Key < GaSpaceDimension
                )
            );

            ScalarValuesDictionary = scalarValuesDictionary;
        }


        public override IEnumerable<GaTerm<double>> GetStoredTerms()
        {
            return ScalarValuesDictionary
                .Select(pair => new GaTerm<double>(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<double>> GetStoredTermsOfGrade(int grade)
        {
            return ScalarValuesDictionary
                .Where(p => p.Key.BasisBladeGrade() == grade)
                .Select(p => new GaTerm<double>(p.Key, p.Value));
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTerms()
        {
            return ScalarValuesDictionary
                .Where(pair => !pair.Value.IsNearZero())
                .Select(pair => new GaTerm<double>(pair.Key, pair.Value));
        }

        public override IEnumerable<GaTerm<double>> GetNonZeroTermsOfGrade(int grade)
        {
            return ScalarValuesDictionary
                .Where(p => p.Key.BasisBladeGrade() == grade && !p.Value.IsNearZero())
                .Select(p => new GaTerm<double>(p.Key, p.Value));
        }


        public override IEnumerable<int> GetStoredTermIds()
        {
            return ScalarValuesDictionary
                .Select(pair => pair.Key);
        }

        public override IEnumerable<int> GetNonZeroTermIds()
        {
            return ScalarValuesDictionary
                .Where(pair => !pair.Value.IsNearZero())
                .Select(pair => pair.Key);
        }

        public override IEnumerable<double> GetStoredTermScalars()
        {
            return ScalarValuesDictionary
                .Select(pair => pair.Value);
        }

        public override IEnumerable<double> GetNonZeroTermScalars()
        {
            return ScalarValuesDictionary
                .Where(pair => !pair.Value.IsNearZero())
                .Select(pair => pair.Value);
        }


        public override bool TryGetValue(int id, out double value)
        {
            return ScalarValuesDictionary.TryGetValue(id, out value);
        }

        public override bool TryGetValue(int grade, int index, out double value)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return ScalarValuesDictionary.TryGetValue(id, out value);
        }

        public override bool TryGetTerm(int id, out GaTerm<double> term)
        {
            if (ScalarValuesDictionary.TryGetValue(id, out var value))
            {
                term = new GaTerm<double>(id, value);
                return true;
            }

            term = null;
            return false;
        }

        public override bool TryGetTerm(int grade, int index, out GaTerm<double> term)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            if (ScalarValuesDictionary.TryGetValue(id, out var value))
            {
                term = new GaTerm<double>(id, value);
                return true;
            }

            term = null;
            return false;
        }


        public override bool IsEmpty()
        {
            return ScalarValuesDictionary.Count == 0;
        }

        public override bool ContainsStoredTerm(int id)
        {
            return id >= 0 && id < GaSpaceDimension &&
                   ScalarValuesDictionary.ContainsKey(id);
        }

        public override bool ContainsStoredTerm(int grade, int index)
        {
            var id = GaFrameUtils.BasisBladeId(grade, index);

            return id >= 0 && id < GaSpaceDimension &&
                   ScalarValuesDictionary.ContainsKey(id);
        }

        public override bool ContainsStoredTermOfGrade(int grade)
        {
            return grade >= 0 && grade <= VSpaceDimension &&
                   ScalarValuesDictionary.Any(p => p.Key.BasisBladeGrade() == grade);
        }


        public override bool IsTerm()
        {
            return ScalarValuesDictionary.Count <= 1;
        }

        public override bool IsScalar()
        {
            return 
                ScalarValuesDictionary.Count == 0 ||
                ScalarValuesDictionary.All(
                    p => p.Key == 0 || p.Value.IsNearZero()
                );
        }

        public override bool IsZero()
        {
            return 
                ScalarValuesDictionary.Count == 0 ||
                ScalarValuesDictionary.Values.All(
                    p => p.IsNearZero()
                );
        }

        public override bool IsNearZero(double epsilon)
        {
            return 
                ScalarValuesDictionary.Count == 0 ||
                ScalarValuesDictionary.Values.All(
                    p => p.IsNearZero()
                );
        }


        public override GaNumDarMultivector GetDarMultivector()
        {
            var scalarValues =
                new SparseReadOnlyList<double>(GaSpaceDimension, ScalarValuesDictionary);

            return new GaNumDarMultivector(scalarValues);
        }

        public override GaNumDgrMultivector GetDgrMultivector()
        {
            var kVectorsArray = new IReadOnlyList<double>[VSpaceDimension + 1];

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                kVectorsArray[grade] =
                    new GaSarMultivectorAsDarKVectorReadOnlyList<double>(
                        VSpaceDimension,
                        grade,
                        ScalarValuesDictionary
                    );
            }

            return new GaNumDgrMultivector(kVectorsArray);
        }

        public override GaNumSarMultivector GetSarMultivector()
        {
            return this;
        }

        public override GaNumSgrMultivector GetSgrMultivector()
        {
            var kVectorsArray = new Dictionary<int, double>[VSpaceDimension + 1];

            var termsList =
                ScalarValuesDictionary
                    .Where(p => !p.Value.IsNearZero());

            foreach (var term in termsList)
            {
                term.Key.BasisBladeGradeIndex(out var grade, out var index);

                if (kVectorsArray[grade] == null)
                    kVectorsArray[grade] = new Dictionary<int, double>();

                kVectorsArray[grade].Add(index, term.Value);
            }

            return new GaNumSgrMultivector(
                kVectorsArray.ToSgrKVectorsList()
            );
        }


        public override IGaNumKVector GetKVectorPart(int grade)
        {
            var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
            var scalarValues = new double[kvSpaceDim];

            var termsList = 
                GetNonZeroTerms().Where(t => t.BasisBladeId.BasisBladeGrade() == grade);

            foreach (var term in termsList)
            {
                var index = term.BasisBladeId.BasisBladeIndex();
                scalarValues[index] = term.ScalarValue;
            }

            return GaNumDarKVector.Create(VSpaceDimension, grade, scalarValues);
        }

        public override IGaNumVector GetVectorPart()
        {
            var scalarValues = new double[VSpaceDimension];

            for (var index = 0; index < VSpaceDimension; index++)
                if (ScalarValuesDictionary.TryGetValue(1 << index, out var value))
                    scalarValues[index] = value;

            return GaNumVector.Create(scalarValues);
        }

        public override IEnumerable<int> GetStoredGrades()
        {
            return GetStoredTermIds().Select(id => id.BasisBladeGrade()).Distinct();
        }

        public override int GetStoredGradesBitPattern()
        {
            return GetStoredTermIds().Aggregate(0, (current, id) => current | (1 << id.BasisBladeGrade()));
        }

        public override IGaGbtNode1<double> GetGbtRootNode()
        {
            return GaGbtBtrMultivectorNode<double>.CreateRootNode(VSpaceDimension, BtrRootNode);
        }

        public override IGaGbtNumMultivectorStack1 CreateGbtStack(int capacity)
        {
            return GaGbtNumSarMultivectorStack1.Create(capacity, this);
        }

        /// <summary>
        /// Extract the even part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumSarMultivector GetEvenGradesPart()
        {
            return GetNonZeroTerms()
                .Where(t => t.BasisBladeId.BasisBladeHasEvenGrade())
                .CreateSarMultivector(VSpaceDimension);
        }

        /// <summary>
        /// Extract the odd part of this multivector as a new multivector
        /// </summary>
        /// <returns></returns>
        public GaNumSarMultivector GetOddGradesPart()
        {
            return GetNonZeroTerms()
                .Where(t => t.BasisBladeId.BasisBladeHasOddGrade())
                .CreateSarMultivector(VSpaceDimension);
        }

        /// <summary>
        /// Extract all k-vector parts of this multivector as a dictionary of
        /// multivectors
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IGaNumKVector> GetKVectorParts()
        {
            var kVectorScalarValuesArray = new double[VSpaceDimension + 1][];

            foreach (var term in GetNonZeroTerms())
            {
                term.BasisBladeId.BasisBladeGradeIndex(out var grade, out var index);

                if (kVectorScalarValuesArray[grade] == null)
                {
                    var kvSpaceDim = GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade);
                    kVectorScalarValuesArray[grade] = new double[kvSpaceDim];
                }

                kVectorScalarValuesArray[grade][index] = term.ScalarValue;
            }

            for (var grade = 0; grade <= VSpaceDimension; grade++)
                if (kVectorScalarValuesArray[grade] != null)
                    yield return GaNumDarKVector.Create(VSpaceDimension, grade, kVectorScalarValuesArray[grade]);
        }


        public override IGaNumMultivector GetNegative()
        {
            var resultMv = new GaNumSarMultivectorFactory(VSpaceDimension);

            foreach (var term in GetNonZeroTerms())
                resultMv.SetTerm(
                    term.BasisBladeId,
                    -term.ScalarValue
                );

            return resultMv.GetSarMultivector();
        }

        public override IGaNumMultivector GetReverse()
        {
            var resultMv = new GaNumSarMultivectorFactory(VSpaceDimension);

            foreach (var term in GetNonZeroTerms())
                resultMv.SetTerm(
                    term.BasisBladeId,
                    term.BasisBladeId.BasisBladeIdHasNegativeReverse() 
                        ? -term.ScalarValue : term.ScalarValue
                );

            return resultMv.GetSarMultivector();
        }

        public override IGaNumMultivector GetGradeInv()
        {
            var resultMv = new GaNumSarMultivectorFactory(VSpaceDimension);

            foreach (var term in GetNonZeroTerms())
                resultMv.SetTerm(
                    term.BasisBladeId,
                    term.BasisBladeId.BasisBladeIdHasNegativeGradeInv() 
                        ? -term.ScalarValue : term.ScalarValue
                );

            return resultMv.GetSarMultivector();
        }

        public override IGaNumMultivector GetCliffConj()
        {
            var resultMv = new GaNumSarMultivectorFactory(VSpaceDimension);

            foreach (var term in GetNonZeroTerms())
                resultMv.SetTerm(
                    term.BasisBladeId,
                    term.BasisBladeId.BasisBladeIdHasNegativeCliffConj() 
                        ? -term.ScalarValue : term.ScalarValue
                );

            return resultMv.GetSarMultivector();
        }


        public override GaNumMultivectorFactory CopyToFactory()
        {
            return new GaNumSarMultivectorFactory(this);
        }


        public override string ToString()
        {
            //return TermsTree.ToString();

            var composer = new LinearTextComposer();

            composer.AppendLine("Binary Tree Multivector:");

            var termsList = 
                GetStoredTerms()
                    .Where(t => t.ScalarValue != 0)
                    .OrderBy(t => t.BasisBladeId);

            foreach (var term in termsList)
                composer
                    .Append(term.BasisBladeId.ToString().PadLeft(12))
                    .Append(": ")
                    .AppendLine(term.ScalarValue.ToString("G"));

            return composer.ToString();
        }
    }
}
