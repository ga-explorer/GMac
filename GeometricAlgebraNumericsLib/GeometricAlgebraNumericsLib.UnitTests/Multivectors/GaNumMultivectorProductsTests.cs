using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;
using GeometricAlgebraStructuresLib.Frames;
using NUnit.Framework;

namespace GeometricAlgebraNumericsLib.UnitTests.Multivectors
{
    [TestFixture]
    public sealed class GaNumMultivectorProductsTests
    {
        private GaRandomGenerator _randomGenerator;

        public int VSpaceDimension { get; } 
            = 10;

        public int GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();


        [OneTimeSetUp]
        public void ClassInit()
        {
            _randomGenerator = new GaRandomGenerator(10);
        }

        private IGaNumMultivector CreateMultivector(string mvKind)
        {
            if (mvKind == "dar")
                return _randomGenerator
                    .GetNumSparseMultivectorTerms(VSpaceDimension)
                    .SumAsDarMultivector(VSpaceDimension);

            if (mvKind == "dgr")
                return _randomGenerator
                    .GetNumSparseMultivectorTerms(VSpaceDimension)
                    .SumAsDgrMultivector(VSpaceDimension);

            if (mvKind == "sar")
                return _randomGenerator
                    .GetNumSparseMultivectorTerms(VSpaceDimension)
                    .SumAsSarMultivector(VSpaceDimension);

            if (mvKind == "sgr")
                return _randomGenerator
                    .GetNumSparseMultivectorTerms(VSpaceDimension)
                    .SumAsSgrMultivector(VSpaceDimension);

            if (mvKind == "darVector")
                return _randomGenerator
                    .GetNumFullVectorTerms(VSpaceDimension)
                    .SumAsDgrMultivector(VSpaceDimension)
                    .GetVectorPart();

            if (mvKind == "darKVector")
                return _randomGenerator
                    .GetNumFullKVectorTerms(VSpaceDimension, VSpaceDimension / 2)
                    .SumAsDgrMultivector(VSpaceDimension)
                    .GetKVectorPart(VSpaceDimension / 2)
                    .ToDarKVector();

            if (mvKind == "sarKVector")
                return _randomGenerator
                    .GetNumSparseKVectorTerms(VSpaceDimension, VSpaceDimension / 2)
                    .SumAsDgrMultivector(VSpaceDimension)
                    .GetKVectorPart(VSpaceDimension / 2)
                    .ToSarKVector();

            if (mvKind == "term")
                return _randomGenerator
                    .GetNumTerm(_randomGenerator.GetInteger(GaSpaceDimension - 1))
                    .ToNumTerm(VSpaceDimension);

            return null;
        }


        [Test]
        public void EGp(
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind1,
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind2
        )
        {
            var mv1 = CreateMultivector(mvKind1);
            var mv2 = CreateMultivector(mvKind2);

            var result1 = mv1.GetGbtEGpTerms(mv2).SumAsDarMultivector(VSpaceDimension);
            var result2 = mv1.GetLoopEGpTerms(mv2).SumAsDarMultivector(VSpaceDimension);

            Assert.IsTrue(result1.IsEqualTo(result2));
        }

        [Test]
        public void Op(
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind1,
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind2
        )
        {
            var mv1 = CreateMultivector(mvKind1);
            var mv2 = CreateMultivector(mvKind2);

            var result1 = mv1.GetGbtOpTerms(mv2).SumAsSarMultivector(VSpaceDimension);
            var result2 = mv1.GetLoopOpTerms(mv2).SumAsSarMultivector(VSpaceDimension);

            Assert.IsTrue(result1.IsEqualTo(result2));
        }

        [Test]
        public void ESp(
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind1,
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind2
        )
        {
            var mv1 = CreateMultivector(mvKind1);
            var mv2 = CreateMultivector(mvKind2);

            var result1 = mv1.GetGbtESpTerms(mv2).SumAsSarMultivector(VSpaceDimension);
            var result2 = mv1.GetLoopESpTerms(mv2).SumAsSarMultivector(VSpaceDimension);

            Assert.IsTrue(result1.IsEqualTo(result2));
        }

        [Test]
        public void ELcp(
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind1,
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind2
        )
        {
            var mv1 = CreateMultivector(mvKind1);
            var mv2 = CreateMultivector(mvKind2);

            var result1 = mv1.GetGbtELcpTerms(mv2).SumAsSarMultivector(VSpaceDimension);
            var result2 = mv1.GetLoopELcpTerms(mv2).SumAsSarMultivector(VSpaceDimension);

            Assert.IsTrue(result1.IsEqualTo(result2));
        }

        [Test]
        public void ERcp(
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind1,
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind2
        )
        {
            var mv1 = CreateMultivector(mvKind1);
            var mv2 = CreateMultivector(mvKind2);

            var result1 = mv1.GetGbtERcpTerms(mv2).SumAsSarMultivector(VSpaceDimension);
            var result2 = mv1.GetLoopERcpTerms(mv2).SumAsSarMultivector(VSpaceDimension);

            Assert.IsTrue(result1.IsEqualTo(result2));
        }

        [Test]
        public void EFdp(
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind1,
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind2
        )
        {
            var mv1 = CreateMultivector(mvKind1);
            var mv2 = CreateMultivector(mvKind2);

            var result1 = mv1.GetGbtEFdpTerms(mv2).SumAsSarMultivector(VSpaceDimension);
            var result2 = mv1.GetLoopEFdpTerms(mv2).SumAsSarMultivector(VSpaceDimension);

            Assert.IsTrue(result1.IsEqualTo(result2));
        }

        [Test]
        public void EHip(
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind1,
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind2
        )
        {
            var mv1 = CreateMultivector(mvKind1);
            var mv2 = CreateMultivector(mvKind2);

            var result1 = mv1.GetGbtEHipTerms(mv2).SumAsSarMultivector(VSpaceDimension);
            var result2 = mv1.GetLoopEHipTerms(mv2).SumAsSarMultivector(VSpaceDimension);

            Assert.IsTrue(result1.IsEqualTo(result2));
        }

        [Test]
        public void EAcp(
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind1,
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind2
        )
        {
            var mv1 = CreateMultivector(mvKind1);
            var mv2 = CreateMultivector(mvKind2);

            var result1 = mv1.GetGbtEAcpTerms(mv2).SumAsSarMultivector(VSpaceDimension);
            var result2 = mv1.GetLoopEAcpTerms(mv2).SumAsSarMultivector(VSpaceDimension);

            Assert.IsTrue(result1.IsEqualTo(result2));
        }

        [Test]
        public void ECp(
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind1,
            [Values("dar", "dgr", "sar", "sgr", "darVector", "darKVector", "sarKVector", "term")] string mvKind2
        )
        {
            var mv1 = CreateMultivector(mvKind1);
            var mv2 = CreateMultivector(mvKind2);

            var result1 = mv1.GetGbtECpTerms(mv2).SumAsSarMultivector(VSpaceDimension);
            var result2 = mv1.GetLoopECpTerms(mv2).SumAsSarMultivector(VSpaceDimension);

            Assert.IsTrue(result1.IsEqualTo(result2));
        }

        [Test]
        public void VectorESquared()
        {
            var v = CreateMultivector("darVector");

            var v2 = v.GetGbtEGpTerms(v).SumAsDarMultivector(VSpaceDimension);

            TestContext.Out.WriteLine($"vector: {v.GetGaNumMultivectorText()}");
            TestContext.Out.WriteLine($"vector2: {v2.GetGaNumMultivectorText()}");

            Assert.IsTrue(v2.IsScalar());
        }

        [Test]
        public void VectorEGpVectorInverse()
        {
            var v = CreateMultivector("darVector").GetSarMultivector();
            var vInv = v.GetGptEInverseTerms().SumAsSarMultivector(VSpaceDimension);

            var v2 = vInv.GetGbtEGpTerms(v).SumAsSarMultivector(VSpaceDimension);

            TestContext.Out.WriteLine($"v: {v.GetGaNumMultivectorText()}");
            TestContext.Out.WriteLine($"inv(v): {vInv.GetGaNumMultivectorText()}");
            TestContext.Out.WriteLine($"inv(v) gp v: {v2.GetGaNumMultivectorText()}");

            Assert.IsTrue(v2.IsScalar());
        }

        [Test]
        public void VersorEGpVersorInverse()
        {
            var v1 = CreateMultivector("darVector").GetSarMultivector();
            var v2 = CreateMultivector("darVector").GetSarMultivector();
            var v3 = CreateMultivector("darVector").GetSarMultivector();

            var v4 = CreateMultivector("darVector");
            var t1 = v4.GetGbtEGpTerms(v4).SumAsDarMultivector(VSpaceDimension);//v1.EGp(v2.EGp(v3.EGp(v3), v2), v1);
            TestContext.Out.WriteLine($"v4: {v4.GetGaNumMultivectorText()}");
            TestContext.Out.WriteLine($"v1 v2 v3 v3 v2 v1: {t1.GetGaNumMultivectorText()}");

            var versor = v1.EGp(v2.EGp(v3));

            var versorInv = versor.Reverse();

            var result = versorInv.EGp(versor);

            TestContext.Out.WriteLine($"versor: {versor.GetGaNumMultivectorText()}");
            TestContext.Out.WriteLine($"inv(versor): {versorInv.GetGaNumMultivectorText()}");
            TestContext.Out.WriteLine($"inv(versor) gp versor: {result.GetGaNumMultivectorText()}");

            Assert.IsTrue(result.IsScalar());
        }
    }
}