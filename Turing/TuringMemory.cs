using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Turing
{
    internal class TuringMemory : IList<char?>
    {
        private readonly List<char?> backList = new List<char?>();
        private readonly List<char?> frontList;

        public char? this[int index]
        {
            get
            {
                try
                {
                    return index >= 0 ? frontList[index] : backList[~index];
                }
                catch (IndexOutOfRangeException)
                {
                    return null;
                }
            }
            set
            {
                var list = GetConvertedIndexAndList(index);
                list.List.Insert(list.Index, value);
            }
        }

        public int Count => backList.Count + frontList.Count;

        public bool IsReadOnly => false;

        public TuringMemory(string str)
        {
            frontList = new List<char?>(str.Select(c => (char?)c));
        }

        private (List<char?> List, int Index) GetConvertedIndexAndList(int index)
        {
            return index >= 0 ? (frontList, index) : (backList, ~index);
        }

        public void Clear()
        {
            backList.Clear();
            frontList.Clear();
        }

        public bool Contains(char? item)
        {
            return backList.Contains(item) || frontList.Contains(item);
        }

        public IEnumerator<char?> GetEnumerator()
        {
            foreach (var item in backList)
            {
                yield return item;
            }
            foreach (var item in frontList)
            {
                yield return item;
            }
        }

        public int IndexOf(char? item)
        {
            var backIndex = backList.IndexOf(item);
            return backIndex != -1 ? backIndex : frontList.IndexOf(item);
        }

        public bool Remove(char? item)
        {
            return backList.Remove(item) || frontList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            var (list, convertedIndex) = GetConvertedIndexAndList(index);
            list.RemoveAt(convertedIndex);
        }

        void ICollection<char?>.Add(char? item)
        {
            throw new NotSupportedException();
        }

        void ICollection<char?>.CopyTo(char?[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        void IList<char?>.Insert(int index, char? item)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
