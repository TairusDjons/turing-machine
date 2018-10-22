using System.Collections.Generic;

namespace Turing
{
    public sealed class TuringMachineFactory : ITuringMachineFactory
    {
        public ITuringMachine Create(IEnumerable<TuringCommand> turingCommands, string memory = null, string state = null)
        {
            return new TuringMachine(turingCommands, memory, state);
        }
    }
}
