﻿using Microsoft.Win32;
using Turing.Services;

namespace Turing.WPF.Services
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        public string Open()
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = "turing";
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
