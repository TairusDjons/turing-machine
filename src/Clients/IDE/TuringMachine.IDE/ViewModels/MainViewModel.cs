using System;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TuringMachine.IDE.Services;
using static TuringMachine.Format;

namespace TuringMachine.IDE.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly ITuringCommandsParser turingCommandsParser;
        private readonly IOpenFileDialogService openFileDialogService;
        private readonly ISaveFileDialogService saveFileDialogService;
        private readonly IErrorDialogService errorDialogService;

        private Memory initialMemory = new Memory();

        public Memory InitialMemory
        {
            get => initialMemory;
            set => Set(ref initialMemory, value);
        }

        private int initialStateNumber = 0;

        public int InitialStateNumber
        {
            get => initialStateNumber;
            set => Set(ref initialStateNumber, value);
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
                TuringMachine.Reset(InitialMemory, InitialStateNumber);
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
                    var filepath = openFileDialogService.Open();
                    if (!(filepath is null))
                    {
                        try
                        {
                            TuringMachine.Commands.Update(turingCommandsParser.Parse(filepath, Encoding.UTF8));
                        }
                        catch (Exception e)
                        {
                            var error = "Ошибка";
                            switch (e)
                            {
                                case UnwrapErrorException ex:
                                    error = "Неверный формат файла";
                                    break;
                            }
                            errorDialogService.Open(error);
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
                        // TODO: write emitter
                        //try
                        //{
                        //    // TuringFormat.Emit(name, TuringMachine.Commands);
                        //}-
                        //catch
                        //{
                        //    errorDialogService.Open("Ошибка сохранения");
                        //}
                    }
                }));

        public MainViewModel(
                ITuringCommandsParser turingCommandsParser,
                IOpenFileDialogService openFileDialogService,
                ISaveFileDialogService saveFileDialogService,
                IErrorDialogService errorDialogService
            )
        {
            TuringMachine = new Machine(InitialMemory, InitialStateNumber);
            this.turingCommandsParser = turingCommandsParser ?? throw new ArgumentNullException(nameof(turingCommandsParser));
            this.openFileDialogService = openFileDialogService ?? throw new ArgumentNullException(nameof(openFileDialogService));
            this.saveFileDialogService = saveFileDialogService ?? throw new ArgumentNullException(nameof(saveFileDialogService));
            this.errorDialogService = errorDialogService ?? throw new ArgumentNullException(nameof(errorDialogService));
        }
    }
}
