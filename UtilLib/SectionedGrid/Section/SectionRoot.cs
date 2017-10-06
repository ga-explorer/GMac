namespace UtilLib.SectionedGrid.Section
{
    public sealed class SectionRoot : SectionParent
    {
        public static SectionRoot CreateRow(SectionedGrid ownerGrid, string sectionRole)
        {
            return new SectionRoot(true, ownerGrid, sectionRole);
        }

        public static SectionRoot CreateRow(SectionedGrid ownerGrid, string sectionRole, int sectionIndex)
        {
            return new SectionRoot(true, ownerGrid, sectionRole, sectionIndex);
        }

        public static SectionRoot CreateColumn(SectionedGrid ownerGrid, string sectionRole)
        {
            return new SectionRoot(false, ownerGrid, sectionRole);
        }

        public static SectionRoot CreateColumn(SectionedGrid ownerGrid, string sectionRole, int sectionIndex)
        {
            return new SectionRoot(false, ownerGrid, sectionRole, sectionIndex);
        }


        private readonly SectionedGrid _parentGrid;


        private SectionRoot(bool isRowSection, SectionedGrid ownerGrid, string sectionRole)
            : base(isRowSection, sectionRole)
        {
            _parentGrid = ownerGrid;
        }

        private SectionRoot(bool isRowSection, SectionedGrid ownerGrid, string sectionRole, int sectionIndex)
            : base(isRowSection, sectionRole, sectionIndex)
        {
            _parentGrid = ownerGrid;
        }


        public override int SectionLevel { get { return 0; } }

        public override SectionRoot RootAncestorSection 
        { 
            get { return this; } 
        }

        public override SectionParent ParentSection
        {
            get { return null; }
        }

        public override SectionedGrid OwnerGrid
        {
            get { return _parentGrid; }
        }

        public override bool IsRoot
        {
            get { return true; }
        }

        public override bool IsInternal
        {
            get { return false; }
        }
    }
}
