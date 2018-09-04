using System.Collections.Generic;

namespace Turing
{
    public interface ITuringMachine
    {
        string Execute(string str, IEnumerable<TuringCommand> turingCommands);
    }
}
