using System.Collections.Generic;

namespace Turing
{
    public interface ITuringMachine
    {
        bool IsEnd { get; }

        int CommandIndex { get; }

        int MemoryIndex { get; }

        IReadOnlyList<char?> Memory { get; }

        void Reset(string str);

        void Step();

        void Execute();
    }
}
