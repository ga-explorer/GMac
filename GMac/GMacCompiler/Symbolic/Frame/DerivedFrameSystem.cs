using GMac.GMacCompiler.Symbolic.GAOM;
using SymbolicInterface.Mathematica;

namespace GMac.GMacCompiler.Symbolic.Frame
{
    public sealed class DerivedFrameSystem
    {
        public GaFrame BaseFrame { get; }

        public GaFrame DerivedFrame { get; }

        public GaOuterMorphism DerivedToBaseOm { get; private set; }

        public GaOuterMorphism BaseToDerivedOm { get; private set; }


        public int VSpaceDim => BaseFrame.VSpaceDimension;

        public int GaSpaceDim => BaseFrame.GaSpaceDimension;

        public ISymbolicMatrix BaseFrameIpm => BaseFrame.Ipm;

        public ISymbolicMatrix DerivedFrameIpm => DerivedFrame.Ipm;


        public DerivedFrameSystem(GaFrame baseFrame, GaFrame derivedFrame, GaOuterMorphism derivedToBaseOm, GaOuterMorphism baseToDerivedOm)
        {
            BaseFrame = baseFrame;
            DerivedFrame = derivedFrame;
            DerivedToBaseOm = derivedToBaseOm;
            BaseToDerivedOm = baseToDerivedOm;
        }

        public DerivedFrameSystem(GaFrame baseFrame, GaFrame derivedFrame, GaOuterMorphism derivedToBaseOm)
        {
            BaseFrame = baseFrame;
            DerivedFrame = derivedFrame;
            DerivedToBaseOm = derivedToBaseOm;
            BaseToDerivedOm = derivedToBaseOm.InverseOm();
        }



    }
}
