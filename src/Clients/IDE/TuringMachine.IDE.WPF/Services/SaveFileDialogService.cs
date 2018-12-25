using System.IO;
using Microsoft.Win32;
using TuringMachine.IDE.Services;

namespace TuringMachine.IDE.WPF.Services
{
    public class SaveFileDialogService : ISaveFileDialogService
    {
        public string Open()
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = ".turing",
                Filter = "Turing files(*.turing)|*.turing|Text files(*.txt)|*.txt|All files(*.*)|*.*",
            };

            if (dialog.ShowDialog() != true)
            {
                return null;
            }
            return dialog.FileName;
        }
    }
}
