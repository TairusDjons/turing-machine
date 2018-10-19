using System;

namespace Turing
{
    public readonly struct TuringState
    {
        public string Name { get; }

        public char? Symbol { get; }

        public TuringState(string name, char? symbol)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name), nameof(name));
            }

            Name = name;
            Symbol = symbol;
        }
    }
}
