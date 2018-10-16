using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Turing.IO;
using Turing.Services;

namespace Turing.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly ITuringCommandParser commandParser;

        private readonly ITuringMachineFactory turingMachineFactory;

        private readonly IOpenFileDialogService openFileDialogService;
        private readonly IErrorDialogService errorDialogService;

        private ITuringMachine turingMachine;

        public ITuringMachine TuringMachine
        {
            get => turingMachine;
            set
            {
                Set(ref turingMachine, value);
                ResetCommand.RaiseCanExecuteChanged();
                StepCommand.RaiseCanExecuteChanged();
                ExecuteCommand.RaiseCanExecuteChanged();
            }
        }

        private string input = "";

        public string Input
        {
            get => input;
            set
            {
                Set(ref input, value);
                TuringMachine?.Reset(value);
            }
        }

        private RelayCommand resetCommand;

        public RelayCommand ResetCommand => resetCommand
            ?? (resetCommand = new RelayCommand(() =>
            {
                TuringMachine.Reset(Input);
                RaisePropertyChanged(nameof(TuringMachine));
            }, () => TuringMachine != null));

        private RelayCommand stepCommand;

        public RelayCommand StepCommand => stepCommand
            ?? (stepCommand = new RelayCommand(() =>
            {
                TuringMachine.Step();
                RaisePropertyChanged(nameof(TuringMachine));
            }, () => TuringMachine?.IsEnd == true));

        private RelayCommand openFileDialogCommand;

        public RelayCommand OpenFileDialogCommand => openFileDialogCommand
                ?? (openFileDialogCommand = new RelayCommand(() =>
                {
                    var name = openFileDialogService.Open();
                    if (name != null)
                    {
                        try
                        {
                            TuringMachine = turingMachineFactory.Create(commandParser.ParseFile(name));
                        }
                        catch (TuringParsingException)
                        {
                            errorDialogService.Open("Неверный формат файла");
                        }
                    }
                }));

        private RelayCommand executeCommand;

        public RelayCommand ExecuteCommand => executeCommand
            ?? (executeCommand = new RelayCommand(() =>
            {
                TuringMachine.Execute();
                RaisePropertyChanged(nameof(TuringMachine));
            }, () => TuringMachine?.IsEnd == true));

        public MainViewModel(
                ITuringCommandParser commandParser,
                ITuringMachineFactory turingMachineFactory,
                IOpenFileDialogService openFileDialogService,
                IErrorDialogService errorDialogService
            )
        {
            this.commandParser = commandParser;
            this.turingMachineFactory = turingMachineFactory;
            this.openFileDialogService = openFileDialogService;
            this.errorDialogService = errorDialogService;
        }
    }
}
