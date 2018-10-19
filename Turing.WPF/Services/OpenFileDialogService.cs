using Microsoft.Win32;
using Turing.Services;

namespace Turing.WPF.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        public string Open()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".turing",
                Filter = "Turing files(*.turing)|*.turing|Text files(*.txt)|*.txt|All files(*.*)|*.*"
            };
            if (dialog.ShowDialog() != true)
            {
                return null;
            }
            return dialog.FileName;
        }
    }
}
