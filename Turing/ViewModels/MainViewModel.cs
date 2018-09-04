using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Turing.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private string input;

        private string output;

        private IEnumerable<TuringCommand> turingCommands;

        private TuringMachine turingMachine;

        public string Input
        {
            get => input;
            set => Set(ref input, value);
        }

        public string Output
        {
            get => output;
            set => Set(ref output, value);
        }

        private RelayCommand executeCommand;

        public RelayCommand ExecuteCommand
        {
            get => executeCommand
                ?? (executeCommand = new RelayCommand(() => Output = turingMachine.Execute(input, turingCommands)));
        }
    }
}
