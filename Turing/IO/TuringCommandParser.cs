using System;
using System.Diagnostics;
using System.IO;

namespace Turing.IO
{
    public class TuringCommandParser : ITuringCommandParser
    {
        private const string LeftTypeString = "l";

        private const string RightTypeString = "r";

        private const string NeutralTypeString = "n";

        public TuringCommand[] ParseFile(string path)
        {
            Debug.Assert(path != null);

            var lines = File.ReadAllLines(path);
            var commands = new TuringCommand[lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                var words = lines[i].Split(' ');
                var type = words[4] == LeftTypeString ? TuringCommandType.Left
                    : words[4] == RightTypeString ? TuringCommandType.Right
                    : words[4] == NeutralTypeString ? TuringCommandType.Neutral
                    : throw new TuringParsingException();
                try
                {
                    commands[i] = new TuringCommand
                        (int.Parse(words[0]), char.Parse(words[1]), int.Parse(words[2]), char.Parse(words[3]), type);
                }
                catch (FormatException exception)
                {
                    throw new TuringParsingException("Bad format", exception);
                }
                catch (OverflowException exception)
                {
                    throw new TuringParsingException("Overflow", exception);
                }
                catch (ArgumentNullException exception)
                {
                    throw new TuringParsingException("Missing argument", exception);
                }
            }
            return commands;
        }
    }
}
