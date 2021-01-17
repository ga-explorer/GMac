using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public static GaPoTSymRotorsSequence Create(IEnumerable<GaPoTSymMultivector> rotorsList)
        {
            return new GaPoTSymRotorsSequence(rotorsList);
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