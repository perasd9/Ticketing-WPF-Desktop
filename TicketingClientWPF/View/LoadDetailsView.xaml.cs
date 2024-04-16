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
using System.Windows.Shapes;
using TicketingClientWPF.ViewModel;

namespace TicketingClientWPF.View
{
    /// <summary>
    /// Interaction logic for LoadDetailsView.xaml
    /// </summary>
    public partial class LoadDetailsView : Window
    {
        public LoadDetailsView()
        {
            InitializeComponent();
        }
        public void ChangePanel(UIElement control)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(control);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Button_MiniMaximize(object sender, RoutedEventArgs e)
        {
            if (MainCoordinator.Instance.loadDetailsView.WindowState != WindowState.Maximized)
                MainCoordinator.Instance.loadDetailsView.WindowState = WindowState.Maximized;
            else
                MainCoordinator.Instance.loadDetailsView.WindowState = WindowState.Normal;
        }
    }
}
