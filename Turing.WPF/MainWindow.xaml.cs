using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using Turing.Messages;
using Turing.ViewModels;


namespace Turing.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<OpenFileDialogMessage>(
                this,
                msg =>
                {
                    var dialog = new OpenFileDialog();
                    if ((bool)dialog.ShowDialog())
                        GetViewModel.ReadFileCommand.Execute(dialog.FileName);
                });
        }

        public MainViewModel GetViewModel => (MainViewModel)DataContext;
        
    }
}
