using System.Collections.Generic;
using System.Linq;

namespace Turing
{
    public sealed class TuringMachine : ITuringMachine
    {
        public Dictionary<TuringState, (TuringState State, TuringCommandType CommandType)> Commands { get; }
            = new Dictionary<TuringState, (TuringState State, TuringCommandType CommandType)>();

        public string StateName { get; set; }

        public int MemoryIndex { get; set; }

        public IList<char?> Memory { get; private set; }

        public TuringMachine(IEnumerable<TuringCommand> turingCommands, string memory = "", string stateName = "")
        {
            Reset(memory, stateName);
            foreach (var command in turingCommands)
            {
                Commands[(command.CurrentState)] = (command.NextState, command.CommandType);
            }
        }

        public void Reset(string memory, string stateName)
        {
            Memory = new TuringMemory(memory);
            StateName = stateName;
            MemoryIndex = 0;
        }

        public bool Step()
        {
            if (Commands.TryGetValue(new TuringState(StateName, Memory[MemoryIndex]), out var output))
            {
                StateName = output.State.Name;
                Memory[MemoryIndex] = output.State.Symbol;
                MemoryIndex += (int)output.CommandType;
                return true;
            }
            return false;
        }

        public void Execute()
        {
            while (Step());
        }
    }
}
