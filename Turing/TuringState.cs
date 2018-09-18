namespace Turing
{
    public readonly struct TuringState
    {
        public int CommandIndex { get; }

        public char? Symbol { get; }

        public TuringState(int commandIndex, char? symbol)
        {
            CommandIndex = commandIndex;
            Symbol = symbol;
        }
    }
}
