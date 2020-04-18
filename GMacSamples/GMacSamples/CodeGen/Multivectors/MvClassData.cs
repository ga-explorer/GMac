using System.Collections.Generic;
using GMac.GMacAPI.Binding;
using GMac.GMacAST.Symbols;
using GMac.GMacUtils;

namespace GMacSamples.CodeGen.Multivectors
{
    internal sealed class MvClassData
    {
        public int ClassId { get; }

        public string ClassName { get; private set; }

        public AstFrame Frame { get; }

        public GMacMultivectorBinding ClassBinding { get; }

        public IEnumerable<int> ClassGrades
        {
            get
            {
                var grade = 0;

                var id = ClassId;

                while (id > 0)
                {
                    if ((id & 1) == 1) yield return grade;

                    grade++;

                    id = id >> 1;
                }
            }
        }

        public IEnumerable<int> ClassBasisBladeIds => Frame.BasisBladeIDsOfGrades(ClassGrades);

        public IEnumerable<AstFrameBasisBlade> ClassBasisBlades => Frame.BasisBladesOfGrades(ClassGrades);


        public MvClassData(AstFrame frame, int id, string name)
        {
            ClassId = id;
            ClassName = name;
            Frame = frame;
            ClassBinding = GMacMultivectorBinding.Create(frame.FrameMultivector);

            if (id < 1) return;

            foreach (var grade in ClassGrades)
                ClassBinding.BindKVectorToVariables(grade);
        }
    }
}
