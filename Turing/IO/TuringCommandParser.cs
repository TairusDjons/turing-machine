using System;
using System.Diagnostics;
using System.IO;

namespace Turing.IO
{
    public class TuringCommandParser : ITuringCommandParser
    {
        public const string LeftTypeString = "l";

        public const string RightTypeString = "r";

        public const string NeutralTypeString = "n";

        public const string EmptySymbol = "null";

        public TuringCommand[] ParseFile(string path)
        {
            Debug.Assert(path != null);

            var lines = File.ReadAllLines(path);
            var commands = new TuringCommand[lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                char? ParseSymbol(string str)
                {
                    return str == "null" ? (char?)null : char.Parse(str);
                }

                try
                {
                    var words = lines[i].Split(' ');
                    var type = words[4] == LeftTypeString ? TuringCommandType.Left
                        : words[4] == RightTypeString ? TuringCommandType.Right
                        : words[4] == NeutralTypeString ? TuringCommandType.Neutral
                        : throw new TuringParsingException();
                    commands[i] = new TuringCommand
                        (int.Parse(words[0]), ParseSymbol(words[1]), int.Parse(words[2]), ParseSymbol(words[3]), type);
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
