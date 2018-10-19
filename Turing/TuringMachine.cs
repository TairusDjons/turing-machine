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

        public string StateName { get; private set; }

        public int MemoryIndex { get; private set; }

        public IReadOnlyList<char?> Memory => new ReadOnlyCollection<char?>(memory.ToList());

        public TuringMachine(IEnumerable<TuringCommand> turingCommands, string str = "", string startStateName = "")
        {
            Reset(str, startStateName);
            foreach (var command in turingCommands)
            {
                commandTransitions[(command.CurrentState)] = (command.NextState, command.CommandType);
            }
        }

        public void Reset(string str, string startStateName)
        {
            memory = new TuringMemory(str);
            StateName = startStateName;
            MemoryIndex = 0;
            IsEnd = true;
        }

        public void Step()
        {
            if ((IsEnd = commandTransitions.TryGetValue(new TuringState(StateName, memory[MemoryIndex]), out var output)))
            {
                StateName = output.State.Name;
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
