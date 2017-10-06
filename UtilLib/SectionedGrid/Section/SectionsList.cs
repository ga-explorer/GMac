using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilLib.SectionedGrid.Section
{
    public sealed class SectionsList : IList<SectionBase>
    {
        private readonly List<SectionBase> _internalList;


        public SectionsList()
        {
            _internalList = new List<SectionBase>();
        }

        public SectionsList(int initialCapacity)
        {
            _internalList = new List<SectionBase>(initialCapacity);
        }

        public SectionsList(SectionBase firstSection)
        {
            _internalList = new List<SectionBase> {firstSection};
        }

        public SectionsList(IEnumerable<SectionBase> sectionsRange)
        {
            _internalList = new List<SectionBase>();

            //This is to ensure that all sections are of the same kind (all rows or all columns)
            foreach (var section in sectionsRange)
                Add(section);
        }


        public SectionBase this[string sectionRole, int sectionIndex]
        {
            get
            {
                var sectionsList =
                    _internalList
                    .Where(
                        section => 
                            section.SectionRole == sectionRole 
                            && section.SectionIndex == sectionIndex
                        );

                foreach (var section in sectionsList)
                    return section;

                throw new IndexOutOfRangeException();
            }
        }

        public bool TryGet(string sectionRole, out SectionBase outSection)
        {
            var sectionsList =
                _internalList
                .Where(
                    section => 
                        section.SectionRole == sectionRole 
                        && section.SectionIndex == -1
                    );

            foreach (var section in sectionsList)
            {
                outSection = section;
                return true;
            }

            outSection = null;
            return false;
        }

        public bool TryGet(string sectionRole, int sectionIndex, out SectionBase outSection)
        {
            var sectionsList =
                _internalList
                .Where(
                    section => 
                        section.SectionRole == sectionRole 
                        && section.SectionIndex == sectionIndex
                    );

            foreach (var section in sectionsList)
            {
                outSection = section;
                return true;
            }

            outSection = null;
            return false;
        }


        //public IEnumerable<SectionBase> Filter(ISectionFilter filter)
        //{
        //    return _InternalList.Where(x => filter.SectionAccepted(x));
        //}

        //public IEnumerable<SectionBase> Filter(Func<SectionBase, bool> predicate)
        //{
        //    return _InternalList.Where(predicate);
        //}

        //public SectionsList FilterToSectionsList(ISectionFilter filter)
        //{
        //    return new SectionsList(_InternalList.Where(x => filter.SectionAccepted(x)));
        //}

        //public SectionsList FilterToSectionsList(Func<SectionBase, bool> predicate)
        //{
        //    return new SectionsList(_InternalList.Where(predicate));
        //}


        public int IndexOf(SectionBase item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, SectionBase item)
        {
            _internalList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _internalList.RemoveAt(index);
        }

        public SectionBase this[int index]
        {
            get
            {
                return _internalList[index];
            }
            set
            {
                _internalList[index] = value;
            }
        }

        public void Add(SectionBase item)
        {
            if (_internalList.Count > 0 && _internalList[0].IsRowSection != item.IsRowSection)
                throw new InvalidOperationException();

            _internalList.Add(item);
        }

        public void Clear()
        {
            _internalList.Clear();
        }

        public bool Contains(SectionBase item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(SectionBase[] array, int arrayIndex)
        {
            _internalList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _internalList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SectionBase item)
        {
            return _internalList.Remove(item);
        }

        public IEnumerator<SectionBase> GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            foreach (var section in _internalList)
                s.AppendLine(section.SectionPath);

            return s.ToString();
        }
    }
}
