using System.Collections.Generic;

namespace Turing
{
    public interface ITuringMachine
    {
        bool IsEnd { get; }

        string StateName { get; }

        int MemoryIndex { get; }

        IReadOnlyList<char?> Memory { get; }

        void Reset(string str, string startCommand);

        void Step();

        void Execute();
    }
}
