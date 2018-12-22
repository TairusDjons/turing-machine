using System.Collections.Generic;

namespace TuringMachine
{
    public class Memory
    {
        private readonly Dictionary<int, char> dict = new Dictionary<int, char>();

        public char PolyfillChar { get; set; }

        public char this[int index]
        {
            get => dict.TryGetValue(index, out var value) ? value : PolyfillChar;
            set
            {
                if (value != PolyfillChar) dict[index] = value;
            }
        }

        public void Clear()
        {
            dict.Clear();
        }
    }
}
