using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TuringMachine.IDE.Services;
using TuringMachine.Format;

namespace TuringMachine.IDE.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly IOpenFileDialogService openFileDialogService;
        private readonly ISaveFileDialogService saveFileDialogService;
        private readonly IErrorDialogService errorDialogService;
        private readonly ITuringEmitter turingMachineFormat;

        private int initialMemoryIndex;

        public int InitialMemoryIndex
        {
            get => initialMemoryIndex;
            set => Set(ref initialMemoryIndex, value);
        }

        private List<char> currentMemory;

        public List<char> CurrentMemory
        {
            get => currentMemory;
            set => Set(ref currentMemory, value);
        }

        private string initialMemory = "0000";

        public string InitialMemory
        {
            get => initialMemory;
            set
            {
                Set(ref initialMemory, value);
                TuringMachine.Memory = GetInitialMemory();
            }
        }

        private int initialMemoryOffset;

        public int InitialMemoryOffset
        {
            get => initialMemoryOffset;
            set => Set(ref initialMemoryOffset, value);
        }

        private int initialStateNumber;

        public int InitialStateNumber
        {
            get => initialStateNumber;
            set => Set(ref initialStateNumber, value);
        }

        private char emptySymbol = '#';

        public char EmptySymbol
        {
            get => emptySymbol;
            set => Set(ref emptySymbol, value);
        }

        private Machine turingMachine;

        public Machine TuringMachine
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
                TuringMachine.Reset(InitialStateNumber, InitialMemoryIndex, GetInitialMemory());
                CurrentMemory = new List<char>(TuringMachine.Memory.dict.Values);
                RaisePropertyChanged(nameof(TuringMachine));
            }, () => !(TuringMachine is null)));

        private RelayCommand stepCommand;

        public RelayCommand StepCommand => stepCommand
            ?? (stepCommand = new RelayCommand(() =>
            {
                TuringMachine.Step();
                CurrentMemory = new List<char>(TuringMachine.Memory.dict.Values);
                RaisePropertyChanged(nameof(TuringMachine));
            }, () => !(TuringMachine is null)));

        private RelayCommand executeCommand;

        public RelayCommand ExecuteCommand => executeCommand
            ?? (executeCommand = new RelayCommand(() =>
            {
                TuringMachine.Execute();
                CurrentMemory = new List<char>(TuringMachine.Memory.dict.Values);
                RaisePropertyChanged(nameof(TuringMachine));
            }, () => !(TuringMachine is null)));

        private RelayCommand openFileDialogCommand;

        public RelayCommand OpenFileDialogCommand => openFileDialogCommand
                ?? (openFileDialogCommand = new RelayCommand(() =>
                {
                    var filepath = openFileDialogService.Open();
                    if (!(filepath is null))
                    {
                        try
                        {
                            TuringMachine.Commands = new Commands(turingMachineFormat.Parse(filepath));
                            RaisePropertyChanged(nameof(TuringMachine));
                        }
                        catch
                        {
                            errorDialogService.Open("Произошла ошибка");
                        }
                    }
                }));

        private RelayCommand saveFileDialogCommand;

        public RelayCommand SaveFileDialogCommand => saveFileDialogCommand
                ?? (saveFileDialogCommand = new RelayCommand(() =>
                {
                    var name = saveFileDialogService.Open();
                    if (!(name is null))
                    {
                        // TODO: write emitter
                        try
                        {
                             turingMachineFormat.Emit(name, TuringMachine.Commands);
                        }
                        catch
                        {
                            errorDialogService.Open("Ошибка сохранения");
                        }
                    }
                }));

        public MainViewModel(
                ITuringEmitter turingMachineFormat,
                IOpenFileDialogService openFileDialogService,
                ISaveFileDialogService saveFileDialogService,
                IErrorDialogService errorDialogService
            )
        {
            TuringMachine = new Machine(InitialStateNumber, InitialMemoryIndex, GetInitialMemory())
            {
                Commands = new Commands()
                {
                    new Command(0, '0', 1, '1', Direction.Right),
                    new Command(1, '0', 0, '2', Direction.Right),
                }
            };
            RaisePropertyChanged(nameof(TuringMachine));
            TuringMachine = new Machine(InitialStateNumber, InitialMemoryIndex, GetInitialMemory());
            this.turingMachineFormat = turingMachineFormat ?? throw new ArgumentNullException(nameof(turingMachineFormat)); ;
            this.openFileDialogService = openFileDialogService ?? throw new ArgumentNullException(nameof(openFileDialogService));
            this.saveFileDialogService = saveFileDialogService ?? throw new ArgumentNullException(nameof(saveFileDialogService));
            this.errorDialogService = errorDialogService ?? throw new ArgumentNullException(nameof(errorDialogService));
        }

        private Memory GetInitialMemory() => new Memory(initialMemory, initialMemoryOffset)
        {
            EmptySymbol = EmptySymbol
        };
    }
}
