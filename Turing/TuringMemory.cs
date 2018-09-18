using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Turing
{
    internal sealed class TuringMemory : IEnumerable<char?>, IEnumerable
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
                catch (ArgumentOutOfRangeException)
                {
                    return null;
                }
            }
            set
            {
                var (list, realIndex) = GetRealIndexAndList(index);
                try
                {
                    list[realIndex] = value;
                }
                catch (ArgumentOutOfRangeException)
                {
                    for (int i = list.Count; i < realIndex; i++) list.Add(null);
                    list.Insert(realIndex, value);
                }
            }
        }

        public TuringMemory(string str)
        {
            frontList = new List<char?>(str.Select(c => (char?)c));
        }

        public void Clear()
        {
            frontList.Clear();
            backList.Clear();
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

        private (List<char?> List, int Index) GetRealIndexAndList(int index)
        {
            return index >= 0 ? (frontList, index) : (backList, ~index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
