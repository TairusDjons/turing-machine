namespace TuringMachine
{
    public class CommandAction
    {
        public char NewSymbol { get; set; }

        public Direction Direction { get; set; }

        public int NextStateNumber { get; set; }

        public CommandAction()
        { }
        public CommandAction(char symbol, Direction direction, int nextState)
        {
            NewSymbol = symbol;
            Direction = direction;
            NextStateNumber = nextState;
        }
    }
}
