namespace TuringMachine
{
    public class Command
    {
        public CommandState State { get; set; }

        public CommandAction Action { get; set; }

        public Command()
        {
        }

        public Command(int stateNumber, char stateSymbol, int nextNumber, char newSymbol, Direction direction)
        {
            State = new CommandState()
            {
                Number = stateNumber,
                Symbol = stateSymbol
            };
            Action = new CommandAction()
            {
                NextNumber = nextNumber,
                NewSymbol = newSymbol,
                Direction = direction
            };
        }
    }
}
