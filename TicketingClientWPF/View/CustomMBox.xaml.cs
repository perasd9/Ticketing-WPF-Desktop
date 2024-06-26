﻿using System;
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

namespace TicketingClientWPF.View
{
    /// <summary>
    /// Interaction logic for CustomMBox.xaml
    /// </summary>
    public partial class CustomMBox : Window
    {
        public CustomMBox()
        {
            InitializeComponent();
        }

        public void ShowDialog(string title, string message)
        {
            this.txtTitle.Text = title;
            this.txtText.Text = message;
            this.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
