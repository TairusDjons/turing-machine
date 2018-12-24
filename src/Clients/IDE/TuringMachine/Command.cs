namespace TuringMachine
{
    public class Command
    {
        public CommandState State { get; set; }

        public CommandAction Action { get; set; }

        public Command(CommandState state, CommandAction action)
        {
            State = state;
            Action = action;
        }
    }
}
