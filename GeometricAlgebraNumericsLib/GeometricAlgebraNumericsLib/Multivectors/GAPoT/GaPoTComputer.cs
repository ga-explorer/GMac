using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraNumericsLib.Frames;
using GeometricAlgebraNumericsLib.Metrics;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;
using GeometricAlgebraNumericsLib.Multivectors.Numeric.Factories;
using GeometricAlgebraNumericsLib.Products;

namespace GeometricAlgebraNumericsLib.Multivectors.GAPoT
{
    //public sealed class GaPoTComputer
    //{
    //    public int VSpaceDimension1 { get; private set; }

    //    public int VSpaceDimension2 { get; private set; }

    //    public GaNumFrameOrthonormal Frame { get; private set; }

    //    public GaNumMetricOrthonormal Metric 
    //        => Frame.OrthonormalMetric;

    //    public int VSpaceDimension
    //        => VSpaceDimension1 + VSpaceDimension2 + 1;

    //    public int GaSpaceDimension1 
    //        => VSpaceDimension1.ToGaSpaceDimension();

    //    public int GaSpaceDimension2
    //        => VSpaceDimension2.ToGaSpaceDimension();

    //    public int GaSpaceDimension
    //        => VSpaceDimension.ToGaSpaceDimension();


    //    public GaPoTComputer(int vSpaceDim1, int vSpaceDim2)
    //    {
    //        Reset(vSpaceDim1, vSpaceDim2);
    //    }


    //    public GaPoTComputer Reset(int vSpaceDim1, int vSpaceDim2)
    //    {
    //        if (VSpaceDimension1 == vSpaceDim1 && VSpaceDimension2 == vSpaceDim2)
    //            return this;

    //        VSpaceDimension1 = vSpaceDim1;
    //        VSpaceDimension2 = vSpaceDim2;

    //        Frame = GaNumFrame.CreateGaPoT(vSpaceDim1, vSpaceDim2);

    //        return this;
    //    }


    //    public GaNumSarMultivector Parse(string mvText)
    //    {
    //        return mvText.ParseGaPoTMultivector(VSpaceDimension1, VSpaceDimension2);
    //    }

    //    public IEnumerable<GaNumSarMultivector> Parse(params string[] mvTextArray)
    //    {
    //        return mvTextArray.Select(t => 
    //            t.ParseGaPoTMultivector(VSpaceDimension1, VSpaceDimension2)
    //        );
    //    }

    //    public IEnumerable<GaNumSarMultivector> Parse(IEnumerable<string> mvTextArray)
    //    {
    //        return mvTextArray.Select(t => 
    //            t.ParseGaPoTMultivector(VSpaceDimension1, VSpaceDimension2)
    //        );
    //    }


    //    public string ToText(GaNumSarMultivector mv)
    //    {
    //        return mv.GetGaPoTMultivectorText(VSpaceDimension1, VSpaceDimension2);
    //    }

    //    public string ToText(GaNumSarMultivector mv, bool useGaPoTFormat)
    //    {
    //        return useGaPoTFormat
    //            ? mv.GetGaPoTMultivectorText(VSpaceDimension1, VSpaceDimension2)
    //            : mv.GetGaNumMultivectorText();
    //    }

    //    public string ToLaTeX(GaNumSarMultivector mv)
    //    {
    //        return mv.GetGaPoTMultivectorLaTeX(VSpaceDimension1, VSpaceDimension2);
    //    }

    //    public string ToLaTeX(GaNumSarMultivector mv, bool useGaPoTFormat)
    //    {
    //        return useGaPoTFormat
    //            ? mv.GetGaPoTMultivectorLaTeX(VSpaceDimension1, VSpaceDimension2)
    //            : mv.GetGaNumMultivectorLaTeX();
    //    }


    //    public GaNumSarMultivector Negative(GaNumSarMultivector mv)
    //    {
    //        return mv.GetNegative().GetSarMultivector();
    //    }

    //    public GaNumSarMultivector Reverse(GaNumSarMultivector mv)
    //    {
    //        return mv.GetReverse().GetSarMultivector();
    //    }

    //    public GaNumSarMultivector GradeInv(GaNumSarMultivector mv)
    //    {
    //        return mv.GetGradeInv().GetSarMultivector();
    //    }

    //    public GaNumSarMultivector CliffConj(GaNumSarMultivector mv)
    //    {
    //        return mv.GetCliffConj().GetSarMultivector();
    //    }


    //    public GaNumSarMultivector Add(GaNumSarMultivector mv1, GaNumSarMultivector mv2)
    //    {
    //        return mv1 + mv2;
    //    }
        
    //    public GaNumSarMultivector Subtract(GaNumSarMultivector mv1, GaNumSarMultivector mv2)
    //    {
    //        return mv1 - mv2;
    //    }

    //    public GaNumSarMultivector ScaleBy(GaNumSarMultivector mv, double s)
    //    {
    //        return s * mv;
    //    }

    //    public GaNumSarMultivector DivideBy(GaNumSarMultivector mv, double s)
    //    {
    //        return (1.0d / s) * mv;
    //    }


    //    public GaNumSarMultivector Gp(GaNumSarMultivector mv1, GaNumSarMultivector mv2)
    //    {
    //        return mv1.GetGbtGpTerms(mv2, Metric).SumAsSarMultivector(VSpaceDimension);
    //    }


    //    public GaNumSarMultivector Gp2(GaNumSarMultivector mv)
    //    {
    //        return mv.GetGbtGpTerms(mv, Metric).SumAsSarMultivector(VSpaceDimension);
    //    }

    //    public double Norm2(GaNumSarMultivector mv)
    //    {
    //        return Math.Abs(mv.GetGbtNorm2(Metric));
    //    }

    //    public double Norm(GaNumSarMultivector mv)
    //    {
    //        return Math.Sqrt(Math.Abs(mv.GetGbtNorm2(Metric)));
    //    }

    //    public GaNumSarMultivector Inverse(GaNumSarMultivector mv)
    //    {
    //        return (1 / Norm2(mv)) * mv;
    //    }
    //}
}
