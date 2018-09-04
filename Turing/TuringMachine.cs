using System.Collections.Generic;
using System.Linq;

namespace Turing
{
    public sealed class TuringMachine
    {
        public string Execute(string str, IEnumerable<TuringCommand> turingCommands)
        {
            var memory = new TuringMemory(str);
            var dict = new Dictionary<(int CommandIndex, char Symbol), (int CommandIndex, char Symbol, TuringCommandType CommandType)>();
            foreach (var command in turingCommands)
            {
                dict[(command.CurrentCommand, command.CurrentSymbol)]
                    = (command.NextCommand, command.NextSymbol, command.CommandType);
            }
            int commandIndex = 0, memoryIndex = 0;
            while (dict.TryGetValue((commandIndex, memory[memoryIndex]), out var output))
            {
                commandIndex = output.CommandIndex;
                memory[memoryIndex] = output.Symbol;
                memoryIndex += (int)output.CommandType;
            }
            return new string(memory.ToArray());
        }
    }
}
