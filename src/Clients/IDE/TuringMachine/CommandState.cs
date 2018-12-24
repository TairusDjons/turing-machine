using System;
namespace TuringMachine
{
    public class CommandState: IEquatable<CommandState>
    {
        public int Number { get; set; }

        public char Symbol { get; set; }

        public CommandState()
        { }
        public CommandState(int number, char symbol)
        {
            Number = number;
            Symbol = symbol;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode() + Symbol.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CommandState other)) return false;
            return Equals(obj as CommandState);
        }

        public bool Equals(CommandState other)
        {
            return this.Number == other.Number &&
                   this.Symbol == other.Symbol;
        }
    }
}
