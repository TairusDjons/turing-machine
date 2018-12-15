using System.Collections.Generic;
using System.Text;

namespace TuringMachine.IDE.Services
{
    public interface ITuringCommandsParser
    {
        IEnumerable<Command> Parse(string path, Encoding encoding);
    }
}
