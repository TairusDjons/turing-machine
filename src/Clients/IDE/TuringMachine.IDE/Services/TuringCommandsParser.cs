using System.Collections.Generic;
using System.Text;
using static TuringMachine.Format.IO;

namespace TuringMachine.IDE.Services
{
    public class TuringCommandsParser : ITuringCommandsParser
    {
        public IEnumerable<Command> Parse(string path, Encoding encoding) => ParseCommands(path, Encoding.UTF8);
    }
}
