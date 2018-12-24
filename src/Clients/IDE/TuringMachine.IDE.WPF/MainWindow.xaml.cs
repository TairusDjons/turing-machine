using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace TuringMachine.IDE.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<char> chars = new ObservableCollection<char>();
        int offset;
        int negOffset;
        public MainWindow()
        {
            InitializeComponent();
            sv.ScrollChanged += inf_scroll;
        }
        private void inf_scroll(object sender, ScrollChangedEventArgs e)
        {
            var some = new List<char>();
            if (lv.ItemsSource is List<char>)
                some = (List<char>)lv.ItemsSource;
            if (some.Count != 0)
            {
                foreach (char c in some)
                {
                    chars.Add(c);
                    
                }
                lv.ItemsSource = chars;
            }
            if (e.HorizontalChange > e.HorizontalOffset)
            {
                chars.Add('#');
                some.Add('#');
            }
            for (int i = 0; i > e.HorizontalChange; i--)
            {
                object tmp = lv.Items[lv.Items.Count - 1];
                chars.Insert(0, '#');
                some.Insert(0, '#');
            }
            sv.ScrollChanged -= inf_scroll;        // remove the handler temporarily
            sv.ScrollToVerticalOffset(sv.VerticalOffset - e.VerticalChange);
            Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => {
                sv.ScrollChanged += inf_scroll;    // add the handler back after the scrolling has occurred to avoid recursive scrolling
            }));

            //lv.ItemsSource = some;
            
        }
    }
}
