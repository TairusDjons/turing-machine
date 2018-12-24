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
            foreach (var c in commands.dict)
            {
                string line = c.Key.Number.ToString() +' ' + c.Key.Symbol + ' '
                             + c.Value.NextStateNumber.ToString() + ' ' + c.Value.NewSymbol
                             + ' ' + c.Value.Direction.ToString();
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
                    commands.Add(new Command(new CommandState(int.Parse(words[0]), char.Parse(words[1])),
                                 new CommandAction(char.Parse(words[3]), type, int.Parse(words[2]))));
                }
                catch(Exception e)
                { }
            }
            return commands;
        }

    }
}
