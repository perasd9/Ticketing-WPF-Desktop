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

namespace TicketingClientWPF.View.Controls.Event
{
    /// <summary>
    /// Interaction logic for UCCreateEvent.xaml
    /// </summary>
    public partial class UCCreateEvent : UserControl
    {
        public event EventHandler GetAllEventTypes;
        public UCCreateEvent()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllEventTypes?.Invoke(this, EventArgs.Empty);
        }
    }
}
