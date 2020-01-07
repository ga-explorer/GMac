using System.Collections.Generic;

namespace UtilLib.SectionedGrid.Section
{
    public abstract class SectionParent : SectionBase
    {
        private readonly SectionsList _childSections = new SectionsList();


        protected SectionParent(bool isRowSection, string sectionRole)
            : base(isRowSection, sectionRole)
        {
        }

        protected SectionParent(bool isRowSection, string sectionRole, int sectionIndex)
            : base(isRowSection, sectionRole, sectionIndex)
        {
        }


        public SectionInternal AddInternalSection(string sectionRole)
        {
            var newSection = SectionInternal.Create(this, sectionRole);

            _childSections.Add(newSection);

            return newSection;
        }

        public SectionInternal AddInternalSection(string sectionRole, int sectionIndex)
        {
            var newSection = SectionInternal.Create(this, sectionRole, sectionIndex);

            _childSections.Add(newSection);

            return newSection;
        }

        public SectionLeaf AddLeafSection(string sectionRole, int sectionIndex)
        {
            var newSection = SectionLeaf.Create(this, sectionRole, sectionIndex);

            _childSections.Add(newSection);

            return newSection;
        }

        public SectionLeaf AddLeafSection(string sectionRole)
        {
            var newSection = SectionLeaf.Create(this, sectionRole);

            _childSections.Add(newSection);

            return newSection;
        }


        //public IEnumerable<SectionBase> Filter(ISectionFilter filter)
        //{
        //    return _ChildSections.Filter(filter);
        //}

        //public IEnumerable<SectionBase> Filter(Func<SectionBase, bool> predicate)
        //{
        //    return _ChildSections.Filter(predicate);
        //}

        //public SectionsList FilterToSectionsList(ISectionFilter filter)
        //{
        //    return _ChildSections.FilterToSectionsList(filter);
        //}

        //public SectionsList FilterToSectionsList(Func<SectionBase, bool> predicate)
        //{
        //    return _ChildSections.FilterToSectionsList(predicate);
        //}


        public override IEnumerable<SectionBase> ChildSections
        {
            get
            {
                return _childSections;
            }
        }

        public override int ChildSectionsCount
        {
            get
            {
                return _childSections.Count;
            }
        }

        public override bool IsLeaf
        {
            get { return false; }
        }

        public override void ClearChildSections()
        {
            _childSections.Clear();
        }
    }
}
