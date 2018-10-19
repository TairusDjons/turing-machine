using System.Collections.Generic;

namespace Turing
{
    public sealed class TuringMachineFactory : ITuringMachineFactory
    {
        public ITuringMachine Create(IEnumerable<TuringCommand> turingCommands, string str = "", string startCommandName = "")
        {
            return new TuringMachine(turingCommands, str, startCommandName);
        }
    }
}
