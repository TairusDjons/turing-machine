namespace TuringMachine
{
    public class Machine
    {
        public int StateNumber { get; set; }

        public int MemoryIndex { get; set; }

        public Memory Memory { get; set; } = new Memory();

        public Commands Commands { get; set; } = new Commands();

        public Machine()
        {
        }

        public Machine(int stateNumber, int memoryIndex, Memory memory)
        {
            Reset(stateNumber, memoryIndex, memory);
        }

        public void Reset(int stateNumber = 0, int memoryIndex = 0, Memory memory = null)
        {
            StateNumber = stateNumber;
            MemoryIndex = memoryIndex;
            if (memory is null)
            {
                Memory.Clear();
            }
            else
            {
                Memory = memory;
            }
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
