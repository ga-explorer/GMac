using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using NUnit.Framework;

namespace GeometricAlgebraNumericsLib.UnitTests.Multivectors
{
    [TestFixture]
    public sealed class GaNumMultivectorFactoryTests
    {
        private GaRandomGenerator _randomGenerator;

        private GaTerm<double>[] _termsArray1;

        private GaTerm<double>[] _termsArray2;

        private GaNumDarMultivector _referenceMultivector1;

        private GaNumDarMultivector _referenceMultivector2;

        public int VSpaceDimension { get; }
            = 10;

        public int GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        [OneTimeSetUp]
        public void ClassInit()
        {
            _randomGenerator = new GaRandomGenerator(10);

            //Create random terms with unique basis blade IDs
            _termsArray1 = 
                _randomGenerator
                    .GetNumSparseMultivectorTerms(VSpaceDimension)
                    .ToArray();

            //Create a multivector from the random terms with unique basis blade IDs
            _referenceMultivector1 = 
                _termsArray1.CreateDarMultivector(VSpaceDimension);

            //Create random terms with non-unique basis blade IDs
            _termsArray2 =
                _randomGenerator
                    .GetNumTerms(VSpaceDimension, VSpaceDimension.ToGaSpaceDimension())
                    .ToArray();

            //Create a multivector from the sum of the random terms with non-unique basis blade IDs
            _referenceMultivector2 =
                _termsArray2.SumAsDarMultivector(VSpaceDimension);
        }


        [Test]
        public void AssertCorrectInitialization()
        {
            Assert.That(_termsArray1, Is.Not.Empty);
            Assert.That(_termsArray2, Is.Not.Empty);

            Assert.That(_referenceMultivector1, Is.Not.Empty);
            Assert.That(_referenceMultivector2, Is.Not.Empty);

            Assert.IsTrue(_termsArray1.Length == _referenceMultivector1.GetNonZeroTerms().Count());
            Assert.IsTrue(_termsArray2.Length > _referenceMultivector2.GetNonZeroTerms().Count());

            TestContext.Out.WriteLine($"Unique Terms Count: {_termsArray1.Length}");
            TestContext.Out.WriteLine($"Unique Terms Multivector Terms Count: {_referenceMultivector1.GetNonZeroTerms().Count()}");
            TestContext.Out.WriteLine("");

            TestContext.Out.WriteLine($"Non-unique Terms Count: {_termsArray2.Length}");
            TestContext.Out.WriteLine($"Non-unique Terms Multivector Terms Count: {_referenceMultivector2.GetNonZeroTerms().Count()}");
        }


        private GaNumMultivectorFactory CreateFactory(string factoryKind)
        {
            if (factoryKind == "dar")
                return new GaNumDarMultivectorFactory(VSpaceDimension);

            if (factoryKind == "dgr")
                return new GaNumDgrMultivectorFactory(VSpaceDimension);

            if (factoryKind == "sar")
                return new GaNumSarMultivectorFactory(VSpaceDimension);

            if (factoryKind == "sgr")
                return new GaNumSgrMultivectorFactory(VSpaceDimension);

            return null;
        }

        private IGaNumMultivector CreateMultivector(IGaNumMultivectorSource source, string multivectorKind, string copyOrGet)
        {
            if (copyOrGet == "copy")
            {
                if (multivectorKind == "dar")
                    return source.GetDarMultivectorCopy();

                if (multivectorKind == "dgr")
                    return source.GetDgrMultivectorCopy();

                if (multivectorKind == "sar")
                    return source.GetSarMultivectorCopy();

                if (multivectorKind == "sgr")
                    return source.GetSgrMultivectorCopy();
            }

            if (multivectorKind == "dar")
                return source.GetDarMultivector();

            if (multivectorKind == "dgr")
                return source.GetDgrMultivector();

            if (multivectorKind == "sar")
                return source.GetSarMultivector();

            if (multivectorKind == "sgr")
                return source.GetSgrMultivector();

            return null;
        }


        [Test, Combinatorial]
        public void SetTerms_FactoryToMultivector(
            [Values("dar", "dgr", "sar", "sgr")] string factoryKind,
            [Values("dar", "dgr", "sar", "sgr")] string multivectorKind,
            [Values("get", "copy")] string getOrCopy
        )
        {
            var factory = 
                CreateFactory(factoryKind);

            factory.SetTerms(_termsArray1);

            var resultMultivector = 
                CreateMultivector(factory, multivectorKind, getOrCopy);

            Assert.IsTrue(_referenceMultivector1.IsEqualTo(resultMultivector));
        }

        [Test, Combinatorial]
        public void AddTerms_FactoryToMultivector(
            [Values("dar", "dgr", "sar", "sgr")] string factoryKind,
            [Values("dar", "dgr", "sar", "sgr")] string multivectorKind,
            [Values("get", "copy")] string getOrCopy
        )
        {
            var factory =
                CreateFactory(factoryKind);

            factory.AddTerms(_termsArray2);

            var resultMultivector =
                CreateMultivector(factory, multivectorKind, getOrCopy);

            Assert.IsTrue(_referenceMultivector2.IsEqualTo(resultMultivector));
        }

        [Test, Combinatorial]
        public void ReadFactoryTermsById(
            [Values("dar", "dgr", "sar", "sgr")] string factoryKind
        )
        {
            var factory =
                CreateFactory(factoryKind).AddTerms(_termsArray1);

            for (var id = 0; id < GaSpaceDimension; id++)
            {
                Assert.IsTrue(
                    factory[id] == _referenceMultivector1[id]
                );
            }
        }

        [Test, Combinatorial]
        public void ReadFactoryTermsByGradeIndex(
            [Values("dar", "dgr", "sar", "sgr")] string factoryKind
        )
        {
            var factory =
                CreateFactory(factoryKind).AddTerms(_termsArray1);

            for (var grade = 0; grade <= VSpaceDimension; grade++)
            {
                var kvSpaceDim = GaNumFrameUtils.KvSpaceDimension(VSpaceDimension, grade);

                for (var index = 0; index < kvSpaceDim; index++)
                {
                    Assert.IsTrue(
                        factory[grade, index] == _referenceMultivector1[grade, index]
                    );
                }
            }
        }
    }
}
