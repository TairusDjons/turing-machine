using System.Collections.Generic;

namespace Turing.IO
{
    public interface ITuringCommandParser
    {
        IEnumerable<TuringCommand> ParseFile(string path);
    }
}
