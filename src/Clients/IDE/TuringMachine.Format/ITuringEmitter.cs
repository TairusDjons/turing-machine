using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachine.Format
{
    public interface ITuringEmitter
    {
        void Emit(string name, Commands commands);
        IEnumerable<Command> Parse(string path);
    }
}
