using System;
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
        private readonly ISaveFileDialogService saveFileDialogService;
        private readonly IErrorDialogService errorDialogService;

        private string initialMemory = "111";

        public string InitialMemory
        {
            get => initialMemory;
            set
            {
                Set(ref initialMemory, value);
            }
        }

        private string initialStateName = "0";

        public string InitialStateName
        {
            get => initialStateName;
            set
            {
                Set(ref initialStateName, value);
            }
        }

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

        private RelayCommand resetCommand;

        public RelayCommand ResetCommand => resetCommand
            ?? (resetCommand = new RelayCommand(() =>
            {
                TuringMachine.Reset(InitialMemory, InitialStateName);
                RaisePropertyChanged(nameof(TuringMachine));
            }, () => !(TuringMachine is null)));

        private RelayCommand stepCommand;

        public RelayCommand StepCommand => stepCommand
            ?? (stepCommand = new RelayCommand(() =>
            {
                TuringMachine.Step();
                RaisePropertyChanged(nameof(TuringMachine));
            }, () => !(TuringMachine is null)));

        private RelayCommand executeCommand;

        public RelayCommand ExecuteCommand => executeCommand
            ?? (executeCommand = new RelayCommand(() =>
            {
                TuringMachine.Execute();
                RaisePropertyChanged(nameof(TuringMachine));
            }, () => !(TuringMachine is null)));

        private RelayCommand openFileDialogCommand;

        public RelayCommand OpenFileDialogCommand => openFileDialogCommand
                ?? (openFileDialogCommand = new RelayCommand(() =>
                {
                    var name = openFileDialogService.Open();
                    if (!(name is null))
                    {
                        try
                        {
                            TuringMachine = turingMachineFactory.Create(commandParser.ParseFile(name), InitialMemory, InitialStateName);
                        }
                        catch (TuringParsingException)
                        {
                            errorDialogService.Open("Неверный формат файла");
                        }
                    }
                }));

        private RelayCommand saveFileDialogCommand;

        public RelayCommand SaveFileDialogCommand => saveFileDialogCommand
                ?? (saveFileDialogCommand = new RelayCommand(() =>
                {
                    var name = openFileDialogService.Open();
                    if (!(name is null))
                    {
                        try
                        {
                            // TuringFormat.Emit(name, TuringMachine.Commands);
                        }
                        catch
                        {
                            errorDialogService.Open("Ошибка сохранения");
                        }
                    }
                }));

        public MainViewModel(
                ITuringCommandParser commandParser,
                ITuringMachineFactory turingMachineFactory,
                IOpenFileDialogService openFileDialogService,
                ISaveFileDialogService saveFileDialogService,
                IErrorDialogService errorDialogService
            )
        {
            this.commandParser = commandParser ?? throw new ArgumentNullException(nameof(commandParser));
            this.turingMachineFactory = turingMachineFactory ?? throw new ArgumentNullException(nameof(turingMachineFactory));
            this.openFileDialogService = openFileDialogService ?? throw new ArgumentNullException(nameof(openFileDialogService));
            this.saveFileDialogService = saveFileDialogService ?? throw new ArgumentNullException(nameof(saveFileDialogService));
            this.errorDialogService = errorDialogService ?? throw new ArgumentNullException(nameof(errorDialogService));
        }
    }
}
