using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Turing.IO;

namespace Turing.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly ITuringCommandParser commandParser;

        private readonly ITuringMachine turingMachine;

        private readonly IEnumerable<TuringCommand> turingCommands;

        private string input;

        public string Input
        {
            get => input;
            set => Set(ref input, value);
        }

        private string output;

        public string Output
        {
            get => output;
            set => Set(ref output, value);
        }

        private RelayCommand executeCommand;

        public RelayCommand ExecuteCommand => executeCommand
                ?? (executeCommand = new RelayCommand(() => Output = turingMachine.Execute(input, turingCommands)));

        public MainViewModel(ITuringCommandParser commandParser, ITuringMachine turingMachine)
        {
            this.commandParser = commandParser;
            this.turingMachine = turingMachine;
        }
    }
}
