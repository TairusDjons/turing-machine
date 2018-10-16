using System.Windows;
using Turing.Services;

namespace Turing.WPF.Services
{
    public class ErrorDialogService : IErrorDialogService
    {
        public void Open(string message)
        {
            MessageBox.Show(message);
        }
    }
}
