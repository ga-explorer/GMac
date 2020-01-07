using System.Collections.Generic;
using System.Text;
using TextComposerLib.DataStructures;
using TextComposerLib.Text.Linear;

namespace UtilLib.SectionedGrid.Section
{
    public abstract class SectionBase
    {
        private static readonly IntegerSequenceGenerator IdCounter = new IntegerSequenceGenerator();

        private static int CreateNewId()
        {
            return IdCounter.GetNewCountId();
        }


        public int SectionId { get; private set; }

        public string SectionRole { get; private set; }

        public int SectionIndex { get; private set; }

        public bool IsRowSection { get; private set; }

        public object AssociatedData = null;

        public int FirstIndex { get; protected set; }

        public int LastIndex { get; protected set; }

        public bool IsColumnSection { get { return !IsRowSection; } }

        public bool IsUpper { get { return IsRoot || IsInternal; } }


        public string SectionName
        {
            get { return SectionIndex < 0 ? SectionRole : SectionRole + "[" + SectionIndex + "]"; }
        }

        public int SectionLength { get { return LastIndex - FirstIndex + 1; } }

        public bool HasLowerSections { get { return ChildSectionsCount > 0; } }

        public string SectionPath
        {
            get
            {
                if (SectionLevel == 0)
                    return SectionName;

                var sectionsList = new List<SectionBase>(SectionLevel);

                var curSection = this;

                //Add every upper section starting at (and including) this section except the root section
                while (curSection.IsRoot == false)
                {
                    sectionsList.Add(curSection);
                    curSection = curSection.ParentSection;
                }

                var s = new StringBuilder();

                //Output the root section
                s.Append(curSection.SectionName);

                //Output the list of sections in reverse order
                for (var i = sectionsList.Count - 1; i >= 0; i--)
                {
                    curSection = sectionsList[i];

                    s.Append(".");
                    s.Append(curSection.SectionName);
                }

                return s.ToString();
            }
        }

        public IEnumerable<SectionParent> AncestorParentSections
        {
            get
            {
                if (IsRoot)
                    yield break;

                for (var section = ParentSection; ReferenceEquals(section, null) == false; section = section.ParentSection)
                    yield return section;
            }
        }

        public IEnumerable<SectionBase> AncestorSections
        {
            get
            {
                if (IsRoot)
                    yield break;

                for (SectionBase section = ParentSection; ReferenceEquals(section, null) == false; section = section.ParentSection)
                    yield return section;
            }
        }

        public virtual IEnumerable<SectionBase> DownChainSections
        {
            get
            {
                var sectionStack = new Stack<SectionBase>();
                sectionStack.Push(this);

                while (sectionStack.Count > 0)
                {
                    var section = sectionStack.Pop();

                    yield return section;

                    if (!section.HasLowerSections) 
                        continue;

                    foreach (var childSection in section.ChildSections)
                        sectionStack.Push(childSection);
                }
            }
        }

        public virtual IEnumerable<SectionBase> DescendantSections
        {
            get
            {
                var sectionStack = new Stack<SectionBase>();

                foreach (var childSection in ChildSections)
                    sectionStack.Push(childSection);

                while (sectionStack.Count > 0)
                {
                    var section = sectionStack.Pop();

                    yield return section;

                    if (!section.HasLowerSections) 
                        continue;

                    foreach (var childSection in section.ChildSections)
                        sectionStack.Push(childSection);
                }
            }
        }

        public IEnumerable<SectionBase> UpChainSections
        {
            get
            {
                for (var section = this; ReferenceEquals(section, null) == false; section = section.ParentSection)
                    yield return section;
            }
        }


        protected SectionBase(bool isRowSection, string sectionRole, int sectionIndex = -1)
        {
            SectionId = CreateNewId();
            IsRowSection = isRowSection;
            SectionRole = sectionRole;
            SectionIndex = sectionIndex;
        }


        private void SectionsTreeToText(LinearComposer log)
        {
            log.Append(SectionName);

            if (IsLeaf)
                return;

            log.AppendLine(" {");
            log.IncreaseIndentation();

            var flag = true;
            foreach (var lowerSection in ChildSections)
            {
                if (flag)
                    flag = false;
                else
                    log.AppendLine(",");

                lowerSection.SectionsTreeToText(log);
            }

            log.DecreaseIndentation();
            log.AppendLine("}");
        }

        public string SectionsTreeToText()
        {
            var log = new LinearComposer();

            SectionsTreeToText(log);

            return log.ToString();
        }


        public virtual int ComputeIndexRange(int firstIndex)
        {
            FirstIndex = firstIndex;
            LastIndex = firstIndex;

            if (ChildSectionsCount <= 0) 
                return LastIndex;

            foreach (var lowerSection in ChildSections)
            {
                lowerSection.ComputeIndexRange(LastIndex + 1);
                LastIndex = lowerSection.LastIndex;
            }

            return LastIndex;
        }

        public override string ToString()
        {
            return SectionPath;
        }


        public abstract SectionedGrid OwnerGrid { get; }

        public abstract int SectionLevel { get; }

        public abstract SectionRoot RootAncestorSection { get; }

        public abstract SectionParent ParentSection { get; }

        public abstract IEnumerable<SectionBase> ChildSections { get; }

        public abstract int ChildSectionsCount { get; }

        public abstract bool IsRoot { get; }

        public abstract bool IsInternal { get; }

        public abstract bool IsLeaf { get; }

        public abstract void ClearChildSections();
    }
}
