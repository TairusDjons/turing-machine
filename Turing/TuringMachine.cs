using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Turing
{
    public sealed class TuringMachine : ITuringMachine
    {
        private TuringMemory memory;

        private readonly Dictionary<TuringState, (TuringState State, TuringCommandType CommandType)> commandTransitions
            = new Dictionary<TuringState, (TuringState State, TuringCommandType CommandType)>();
        
        public bool IsEnd { get; private set; }

        public int CommandIndex { get; private set; }

        public int MemoryIndex { get; private set; }

        public IReadOnlyList<char?> Memory => new ReadOnlyCollection<char?>(memory.ToList());

        public TuringMachine(IEnumerable<TuringCommand> turingCommands, string str = "")
        {
            Reset(str);
            foreach (var command in turingCommands)
            {
                commandTransitions[(command.CurrentState)] = (command.NextState, command.CommandType);
            }
        }

        public void Reset(string str)
        {
            memory = new TuringMemory(str);
            CommandIndex = 0;
            MemoryIndex = 0;
        }

        public void Step()
        {
            if ((IsEnd = commandTransitions.TryGetValue(new TuringState(CommandIndex, memory[MemoryIndex]), out var output)))
            {
                CommandIndex = output.State.CommandIndex;
                memory[MemoryIndex] = output.State.Symbol;
                MemoryIndex += (int)output.CommandType;
            }
        }

        public void Execute()
        {
            do { Step(); } while (IsEnd);
        }
    }
}
