using System.Collections.Generic;

namespace TuringMachine
{
    public class Memory
    {
        public Dictionary<int, char> dict = new Dictionary<int, char>();

        public char EmptySymbol { get; set; }

        public char this[int index]
        {
            get => dict.TryGetValue(index, out var value) ? value : EmptySymbol;
            set
            {
                if (value != EmptySymbol) dict[index] = value;
            }
        }

        public void Clear()
        {
            dict.Clear();
        }

        public Memory()
        {
        }

        public Memory(string str, int offset = 0)
        {
            foreach (var symbol in str)
            {
                this[offset++] = symbol;
            }
        }
    }
}
