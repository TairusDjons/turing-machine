using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using Turing.Messages;
using Turing.IService;

namespace Turing.WPF.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        public void Open()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
                Messenger.Default.Send(new FilePathMessage(dialog.FileName));
        }
    }
}
