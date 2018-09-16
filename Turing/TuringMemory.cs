using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Turing
{
    internal class TuringMemory : IEnumerable<char?>, IEnumerable
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

        public TuringMemory(string str)
        {
            frontList = new List<char?>(str.Select(c => (char?)c));
        }

        private (List<char?> List, int Index) GetConvertedIndexAndList(int index)
        {
            return index >= 0 ? (frontList, index) : (backList, ~index);
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
