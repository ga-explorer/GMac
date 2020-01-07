using System.Collections.Generic;

namespace UtilLib.SectionedGrid.Section
{
    public sealed class SectionLeaf : SectionBase
    {
        public static SectionLeaf Create(SectionParent parentSection, string sectionRole)
        {
            return new SectionLeaf(parentSection.IsRowSection, parentSection, sectionRole);
        }

        public static SectionLeaf Create(SectionParent parentSection, string sectionRole, int sectionIndex)
        {
            return new SectionLeaf(parentSection.IsRowSection, parentSection, sectionRole, sectionIndex);
        }


        private readonly SectionParent _parentSection;


        private SectionLeaf(bool isRowSection, SectionParent parentSection, string sectionRole)
            : base(isRowSection, sectionRole)
        {
            _parentSection = parentSection;
        }

        private SectionLeaf(bool isRowSection, SectionParent parentSection, string sectionRole, int sectionIndex)
            : base(isRowSection, sectionRole, sectionIndex)
        {
            _parentSection = parentSection;
        }


        public override int SectionLevel
        {
            get { return _parentSection.SectionLevel + 1; }
        }

        public override SectionRoot RootAncestorSection
        {
            get { return _parentSection.RootAncestorSection; }
        }

        public override SectionParent ParentSection
        {
            get { return _parentSection; }
        }

        public override IEnumerable<SectionBase> ChildSections
        {
            get { yield break; }
        }

        public override IEnumerable<SectionBase> DownChainSections
        {
            get { yield return this; }
        }

        public override IEnumerable<SectionBase> DescendantSections
        {
            get { yield break; }
        }

        public override int ChildSectionsCount
        {
            get
            {
                return 0;
            }
        }

        public override SectionedGrid OwnerGrid
        {
            get { return _parentSection.OwnerGrid; }
        }

        public override bool IsRoot
        {
            get { return false; }
        }

        public override bool IsInternal
        {
            get { return false; }
        }

        public override bool IsLeaf
        {
            get { return false; }
        }

        public override void ClearChildSections()
        {
        }

        public override int ComputeIndexRange(int firstIndex)
        {
            FirstIndex = firstIndex;
            LastIndex = firstIndex;

            return LastIndex;
        }
    }
}
