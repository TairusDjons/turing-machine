using System.Windows;
using TuringMachine.IDE.Services;

namespace TuringMachine.IDE.WPF.Services
{
    public class ErrorDialogService : IErrorDialogService
    {
        public void Open(string message)
        {
            MessageBox.Show(message);
        }
    }
}
