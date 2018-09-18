using System.Collections.Generic;

namespace Turing
{
    public sealed class TuringMachineFactory : ITuringMachineFactory
    {
        public ITuringMachine Create(IEnumerable<TuringCommand> turingCommands, string str = "")
        {
            return new TuringMachine(turingCommands, str);
        }
    }
}
