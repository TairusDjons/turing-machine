namespace TuringMachine
{
    public class CommandAction
    {
        public char NewSymbol { get; set; }

        public Direction Direction { get; set; }

        public int NextStateNumber { get; set; }
    }
}
