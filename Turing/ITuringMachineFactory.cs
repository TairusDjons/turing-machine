using System.Collections.Generic;

namespace Turing
{
    public interface ITuringMachineFactory
    {
        ITuringMachine Create(IEnumerable<TuringCommand> turingCommands, string str = "");
    }
}
