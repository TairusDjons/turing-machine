namespace Turing
{
    public sealed class TuringCommand
    {
        public TuringState CurrentState { get; }

        public TuringState NextState { get; }

        public TuringCommandType CommandType { get; }

        public TuringCommand(string currentState, char? currentSymbol, string nextState, char? nextSymbol, TuringCommandType commandType)
        {
            CurrentState = new TuringState(currentState, currentSymbol);
            NextState = new TuringState(nextState, nextSymbol);
            CommandType = commandType;
        }
    }
}
