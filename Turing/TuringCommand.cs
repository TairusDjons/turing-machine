namespace Turing
{
    public sealed class TuringCommand
    {
        public int CurrentCommand { get; }

        public char CurrentSymbol { get; }

        public int NextCommand { get; }

        public char NextSymbol { get; }

        public TuringCommandType TuringCommandType { get; }
    }
}
