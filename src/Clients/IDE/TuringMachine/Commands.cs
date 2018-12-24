using System.Collections.Generic;

namespace TuringMachine
{
    public class Commands : List<Command>
    {
        public Commands()
        {
        }

        public Commands(IEnumerable<Command> collection) : base(collection)
        {
        }

        public Commands(int capacity) : base(capacity)
        {
        }

        public CommandAction GetAction(CommandState state)
        {
            return Find(it => it.State == state).Action;
        }
    }
}
