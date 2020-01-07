namespace UtilLib.SectionedGrid.Section
{
    public sealed class SectionInternal : SectionParent
    {
        public static SectionInternal Create(SectionParent parentSection, string sectionRole)
        {
            return new SectionInternal(parentSection.IsRowSection, parentSection, sectionRole);
        }

        public static SectionInternal Create(SectionParent parentSection, string sectionRole, int sectionIndex)
        {
            return new SectionInternal(parentSection.IsRowSection, parentSection, sectionRole, sectionIndex);
        }


        private readonly SectionParent _parentSection;


        private SectionInternal(bool isRowSection, SectionParent parentSection, string sectionRole)
            : base(isRowSection, sectionRole)
        {
            _parentSection = parentSection;
        }

        private SectionInternal(bool isRowSection, SectionParent parentSection, string sectionRole, int sectionIndex)
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
            get { return true; }
        }
    }
}
