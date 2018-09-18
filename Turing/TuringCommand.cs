namespace Turing
{
    public sealed class TuringCommand
    {
        public TuringState CurrentState { get; }

        public TuringState NextState { get; }

        public TuringCommandType CommandType { get; }

        public TuringCommand(int currentCommand, char? currentSymbol, int nextCommand, char? nextSymbol, TuringCommandType commandType)
        {
            CurrentState = new TuringState(currentCommand, currentSymbol);
            NextState = new TuringState(nextCommand, nextSymbol);
            CommandType = commandType;
        }
    }
}
