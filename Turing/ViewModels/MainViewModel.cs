using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using Turing.IO;

namespace Turing.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly ITuringCommandParser commandParser;

        private readonly ITuringMachineFactory turingMachineFactory;

        private readonly IEnumerable<TuringCommand> turingCommands;

        private ITuringMachine turingMachine;

        public ITuringMachine TuringMachine
        {
            get => turingMachine;
            set => Set(ref turingMachine, value);
        }

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

        private RelayCommand resetCommand;

        public RelayCommand ResetCommand => resetCommand
            ?? (resetCommand = new RelayCommand(() =>
            {
                TuringMachine.Reset(Input);
            }, () => TuringMachine != null));

        private RelayCommand stepCommand;

        public RelayCommand StepCommand => stepCommand
            ?? (stepCommand = new RelayCommand(() =>
            {
                TuringMachine.Step();
            }, () => TuringMachine?.IsEnd == true));

        private RelayCommand executeCommand;

        public RelayCommand ExecuteCommand => executeCommand
            ?? (executeCommand = new RelayCommand(() =>
            {
                TuringMachine.Execute();
            }, () => TuringMachine?.IsEnd == true));

        public MainViewModel(ITuringCommandParser commandParser, ITuringMachineFactory turingMachineFactory)
        {
            this.commandParser = commandParser;
            this.turingMachineFactory = turingMachineFactory;
        }
    }
}
