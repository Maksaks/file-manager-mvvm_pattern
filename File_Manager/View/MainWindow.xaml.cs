using File_Manager.ViewModel;
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

namespace File_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainVM;
        public MainWindow()
        {
            InitializeComponent();
            mainVM = new MainViewModel();
            DataContext = mainVM;
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_ChangeState(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Maximized(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal) { 
                WindowState = WindowState.Maximized;
            } else
            {
                WindowState = WindowState.Normal;
            }
        }
    }
}
