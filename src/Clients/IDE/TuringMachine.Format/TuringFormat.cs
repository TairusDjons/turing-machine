using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TuringMachine.Format
{
    public class TuringFormat : ITuringEmitter
    {
        public void Emit(string name, Commands commands)
        {
            var fileText = new List<string>();
            foreach (var c in commands)
            {
                var type = c.Action.Direction == Direction.Left ? "l"
                    : c.Action.Direction == Direction.Right ? "r"
                    : c.Action.Direction == Direction.Pause ? "n"
                    : throw new Exception();
                string line = c.State.Number.ToString() +' ' + c.State.Symbol + ' '
                             + c.Action.NextNumber.ToString() + ' ' + c.Action.NewSymbol
                             + ' ' + type;
                fileText.Add(line);
            }
            File.WriteAllLines(name, fileText);
        }

        public IEnumerable<Command> Parse(string path)
        {
            var lines = File.ReadAllLines(path);
            var commands = new List<Command>();
            foreach (var line in lines)
            {
                var words = line.Split(' ');
                var type = words[4] == "l" ? Direction.Left
                    : words[4] == "r" ? Direction.Right
                    : words[4] == "n" ? Direction.Pause
                    : throw new Exception();
                try
                {
                    commands.Add(new Command(int.Parse(words[0]), char.Parse(words[1]),
                                 int.Parse(words[2]), char.Parse(words[3]), type));
                }
                catch(Exception e)
                { }
            }
            return commands;
        }

    }
}
