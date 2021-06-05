using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymRotorsSequence : IReadOnlyList<GaPoTSymMultivector>
    {
        public static GaPoTSymRotorsSequence CreateIdentity()
        {
            return new GaPoTSymRotorsSequence();

            //return new GaPoTSymRotorsSequence().AppendRotor(
            //    GaPoTSymMultivector.CreateZero().SetTerm(0, Expr.INT_ONE)
            //);
        }

        public static GaPoTSymRotorsSequence Create(params GaPoTSymMultivector[] rotorsList)
        {
            return new GaPoTSymRotorsSequence(rotorsList);
        }

        public static GaPoTSymRotorsSequence Create(IEnumerable<GaPoTSymMultivector> rotorsList)
        {
            return new GaPoTSymRotorsSequence(rotorsList);
        }

        public static GaPoTSymRotorsSequence CreateFromOrthonormalFrames(GaPoTSymFrame sourceFrame, GaPoTSymFrame targetFrame, bool fullRotorsFlag = false)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            //Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var rotorsSequence = new GaPoTSymRotorsSequence();

            var sourceFrameVectors = sourceFrame.ToArray();

            var n = fullRotorsFlag 
                ? sourceFrame.Count 
                : sourceFrame.Count - 1;

            for (var i = 0; i < n; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrame[i];

                var rotor = GaPoTSymMultivector.CreateSimpleRotor(
                    sourceVector, 
                    targetVector
                );

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = sourceFrameVectors[j].ApplyRotor(rotor);
            }

            return rotorsSequence;
        }

        public static GaPoTSymRotorsSequence CreateFromOrthonormalFrames(GaPoTSymFrame sourceFrame, GaPoTSymFrame targetFrame, int[] sequenceArray)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            Debug.Assert(sourceFrame.IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            Debug.Assert(sequenceArray.Length == sourceFrame.Count - 1);
            Debug.Assert(sequenceArray.Min() >= 0);
            Debug.Assert(sequenceArray.Max() < sourceFrame.Count);
            Debug.Assert(sequenceArray.Distinct().Count() == sourceFrame.Count - 1);

            var rotorsSequence = new GaPoTSymRotorsSequence();

            var sourceFrameVectors = sourceFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var vectorIndex = sequenceArray[i];

                var sourceVector = sourceFrameVectors[vectorIndex];
                var targetVector = targetFrame[vectorIndex];

                var rotor = GaPoTSymMultivector.CreateSimpleRotor(
                    sourceVector, 
                    targetVector
                );

                rotorsSequence.AppendRotor(rotor);

                for (var j = i + 1; j < sourceFrame.Count; j++)
                    sourceFrameVectors[j] = sourceFrameVectors[j].ApplyRotor(rotor);
            }

            return rotorsSequence;
        }

        public static GaPoTSymRotorsSequence CreateFromFrames(int baseSpaceDimensions, GaPoTSymFrame sourceFrame, GaPoTSymFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == sourceFrame.Count);
            //Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            //Debug.Assert(sourceFrame.HasSameHandedness(targetFrame));

            var rotorsSequence = new GaPoTSymRotorsSequence();

            var pseudoScalar = 
                GaPoTSymMultivector.CreateTerm(
                    (1UL << baseSpaceDimensions) - 1, 
                    Expr.INT_ONE
                );

            var sourceFrameVectors = sourceFrame.ToArray();
            var targetFrameVectors = targetFrame.ToArray();
            
            for (var i = 0; i < sourceFrame.Count - 1; i++)
            {
                var sourceVector = sourceFrameVectors[i];
                var targetVector = targetFrameVectors[i];

                var rotor = GaPoTSymMultivector.CreateSimpleRotor(
                    sourceVector, 
                    targetVector
                );

                rotorsSequence.AppendRotor(rotor);

                pseudoScalar = targetVector.ToMultivector().Lcp(pseudoScalar.Inverse());

                for (var j = i + 1; j < sourceFrame.Count; j++)
                {
                    sourceFrameVectors[j] = 
                        sourceFrameVectors[j].ApplyRotor(rotor).GetProjectionOnBlade(pseudoScalar);

                    targetFrameVectors[j] =
                        targetFrameVectors[j].GetProjectionOnBlade(pseudoScalar);
                }
            }

            return rotorsSequence;
        }


        private readonly List<GaPoTSymMultivector> _rotorsList
            = new List<GaPoTSymMultivector>();


        public int Count 
            => _rotorsList.Count;
        
        public GaPoTSymMultivector this[int index]
        {
            get => _rotorsList[index];
            set => _rotorsList[index] = value;
        }

        
        internal GaPoTSymRotorsSequence()
        {
        }

        internal GaPoTSymRotorsSequence(IEnumerable<GaPoTSymMultivector> rotorsList)
        {
            _rotorsList.AddRange(rotorsList);
        }


        public bool ValidateRotation(GaPoTSymFrame sourceFrame, GaPoTSymFrame targetFrame)
        {
            if (sourceFrame.Count != targetFrame.Count)
                return false;

            var rotatedFrame = Rotate(sourceFrame);

            return !rotatedFrame.Where(
                (v, i) => !(targetFrame[i] - v).IsZero()
            ).Any();
        }

        public bool IsRotorsSequence()
        {
            return _rotorsList.All(r => r.IsRotor());
        }

        public bool IsSimpleRotorsSequence()
        {
            return _rotorsList.All(r => r.IsSimpleRotor());
        }

        public GaPoTSymRotorsSequence AppendRotor(GaPoTSymMultivector rotor)
        {
            _rotorsList.Add(rotor);

            return this;
        }

        public GaPoTSymRotorsSequence PrependRotor(GaPoTSymMultivector rotor)
        {
            _rotorsList.Insert(0, rotor);

            return this;
        }

        public GaPoTSymRotorsSequence InsertRotor(int index, GaPoTSymMultivector rotor)
        {
            _rotorsList.Insert(index, rotor);

            return this;
        }

        public GaPoTSymRotorsSequence GetSubSequence(int startIndex, int count)
        {
            return new GaPoTSymRotorsSequence(
                _rotorsList.Skip(startIndex).Take(count)
            );
        }

        public GaPoTSymRotorsSequence JoinRotorPairs()
        {
            var rotorsSequence = new GaPoTSymRotorsSequence();

            var pairsCount = 
                _rotorsList.Count % 2 == 0
                    ? _rotorsList.Count / 2
                    : (_rotorsList.Count - 1) / 2;

            for (var i = 0; i < pairsCount; i++)
            {
                var rotor1 = _rotorsList[2 * i];
                var rotor2 = _rotorsList[2 * i + 1];

                rotorsSequence.AppendRotor(
                    rotor2.Gp(rotor1)
                );
            }

            if (_rotorsList.Count % 2 == 1)
                rotorsSequence.AppendRotor(
                    _rotorsList[_rotorsList.Count - 1]
                );

            return rotorsSequence;
        }

        public IEnumerable<GaPoTSymVector> GetRotations(GaPoTSymVector vector)
        {
            var v = vector;

            yield return v;

            foreach (var rotor in _rotorsList)
            {
                v = v.ApplyRotor(rotor);

                yield return v;
            }
        }

        public IEnumerable<GaPoTSymMultivector> GetRotations(GaPoTSymMultivector multivector)
        {
            var mv = multivector;

            yield return mv;

            foreach (var rotor in _rotorsList)
            {
                mv = mv.ApplyRotor(rotor);

                yield return mv;
            }
        }

        public IEnumerable<GaPoTSymFrame> GetRotations(GaPoTSymFrame frame)
        {
            var f = frame;

            yield return f;

            foreach (var rotor in _rotorsList)
            {
                f = f.ApplyRotor(rotor);

                yield return f;
            }
        }

        public IEnumerable<Expr> GetRotationMatrices(int rowsCount)
        {
            var f = GaPoTSymFrame.CreateBasisFrame(rowsCount);

            yield return f.GetMatrix(rowsCount).ToArrayExpr();

            foreach (var rotor in _rotorsList)
                yield return f.ApplyRotor(rotor).GetMatrix(rowsCount).ToArrayExpr();
        }

        public GaPoTSymVector Rotate(GaPoTSymVector vector)
        {
            return _rotorsList
                .Aggregate(
                    vector, 
                    (current, rotor) => current.ApplyRotor(rotor)
                );
        }

        public GaPoTSymMultivector Rotate(GaPoTSymMultivector multivector)
        {
            return _rotorsList
                .Aggregate(
                    multivector, 
                    (current, rotor) => current.ApplyRotor(rotor)
                );
        }

        public GaPoTSymFrame Rotate(GaPoTSymFrame frame)
        {
            return _rotorsList
                .Aggregate(
                    frame, 
                    (current, rotor) => current.ApplyRotor(rotor)
                );
        }

        public GaPoTSymMultivector GetFinalRotor()
        {
            return _rotorsList
                .Skip(1)
                .Aggregate(
                    _rotorsList[0], 
                    (current, rotor) => rotor.Gp(current)
                );
        }

        public Expr GetFinalMatrixExpr(int rowsCount)
        {
            return Rotate(
                GaPoTSymFrame.CreateBasisFrame(rowsCount)
                )
                .GetMatrix(rowsCount)
                .ToArrayExpr();
        }

        public GaPoTSymRotorsSequence Reverse()
        {
            var rotorsSequence = new GaPoTSymRotorsSequence();

            foreach (var rotor in _rotorsList)
                rotorsSequence.PrependRotor(rotor.Reverse());

            return rotorsSequence;
        }

        public string ToLaTeXEquationsArrays(string rotorsName, string basisName)
        {
            var textComposer = new StringBuilder();

            for (var i = 0; i < Count; i++)
            {
                var rotorEquation = this[i].ToLaTeXEquationsArray(
                    $"{rotorsName}_{{{i + 1}}}", 
                    basisName
                );

                textComposer.AppendLine(rotorEquation);
                textComposer.AppendLine();
            }

            return textComposer.ToString();
        }


        public IEnumerator<GaPoTSymMultivector> GetEnumerator()
        {
            return _rotorsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}