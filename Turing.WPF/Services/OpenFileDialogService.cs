using Microsoft.Win32;
using Turing.IService;

namespace Turing.WPF.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        public string Open()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
