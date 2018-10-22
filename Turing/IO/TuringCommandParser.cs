using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Turing.IO
{
    public class TuringCommandParser : ITuringCommandParser
    {
        public const string LeftTypeString = "l";

        public const string RightTypeString = "r";

        public const string NeutralTypeString = "n";

        public const string EmptySymbol = "null";

        public static readonly IReadOnlyCollection<char> Separators = new ReadOnlyCollection<char>(new [] { ' ', '\t' });

        public IEnumerable<TuringCommand> ParseFile(string path)
        {
            Debug.Assert(path != null);

            var lines = File.ReadAllLines(path);
            var commands = new List<TuringCommand>(lines.Length);
            for (var i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                TuringCommandType ParseCommandType(string word)
                {
                    return word == LeftTypeString ? TuringCommandType.Left
                        : word == RightTypeString ? TuringCommandType.Right
                        : word == NeutralTypeString ? TuringCommandType.Neutral
                        : throw new TuringParsingException();
                }

                char? ParseSymbol(string str)
                {
                    return str == EmptySymbol ? (char?)null : char.Parse(str);
                }

                try
                {
                    var words = lines[i].Split(Separators.ToArray());
                    commands.Add(new TuringCommand
                        (words[0], ParseSymbol(words[1]), words[2], ParseSymbol(words[3]), ParseCommandType(words[4])));
                }
                catch
                {
                    throw new TuringParsingException("Error");
                }
            }
            return commands;
        }
    }
}
