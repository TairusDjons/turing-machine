namespace TuringMachine
{
    public class Machine
    {
        public int StateNumber { get; set; }

        public int MemoryIndex { get; set; }

        public Memory Memory { get; set; } = new Memory();

        public Commands Commands { get; set; } = new Commands();

        public void Reset(int stateNumber, Memory memory)
        {
            StateNumber = stateNumber;
            Memory = memory;
        }

        public bool Step()
        {
            if (Commands.TryGetAction(new CommandState() { Number = StateNumber, Symbol = Memory[MemoryIndex] }, out var action))
            {
                StateNumber = action.NextStateNumber;
                Memory[MemoryIndex] = action.NewSymbol;
                MemoryIndex += (int)action.Direction;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Execute()
        {
            while (Step()) { }
        }
    }
}
