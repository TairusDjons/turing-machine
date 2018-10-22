using System.Collections.Generic;

namespace Turing
{
    public interface ITuringMachine
    {
        string StateName { get; set; }

        int MemoryIndex { get; set; }

        IList<char?> Memory { get; }

        Dictionary<TuringState, (TuringState State, TuringCommandType CommandType)> Commands { get; }

        void Reset(string memory = null, string stateName = null);

        bool Step();

        void Execute();
    }
}
