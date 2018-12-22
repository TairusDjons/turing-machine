using System.Collections.Generic;

namespace TuringMachine
{
    public class Commands
    {
        private readonly Dictionary<CommandState, CommandAction> dict = new Dictionary<CommandState, CommandAction>();

        public CommandAction this[CommandState index]
        {
            get => dict[index];
            set => dict[index] = value;
        }

        public Commands()
        {
        }

        public Commands(IEnumerable<Command> commands)
        {
            Update(commands);
        }

        public void Clear() => dict.Clear();

        public void Update(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                this[command.State] = command.Action;
            }
        }

        public bool TryGetAction(CommandState state, out CommandAction action)
            => dict.TryGetValue(state, out action);
    }
}
