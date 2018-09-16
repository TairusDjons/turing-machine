namespace Turing
{
    public class TuringCommand
    {
        public int CurrentCommand { get; }

        public char? CurrentSymbol { get; }

        public int NextCommand { get; }

        public char? NextSymbol { get; }

        public TuringCommandType CommandType { get; }

        public TuringCommand(int currentCommand, char? currentSymbol, int nextCommand, char? nextSymbol, TuringCommandType commandType)
        {
            CurrentCommand = currentCommand;
            CurrentSymbol = currentSymbol;
            NextCommand = nextCommand;
            NextSymbol = nextSymbol;
            CommandType = commandType;
        }
    }
}
