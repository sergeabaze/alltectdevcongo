﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AllTech.FrameWork.Views
{
    /// <summary>
    /// Interaction logic for StyledMessageBoxView.xaml
    /// </summary>
    public partial class StyledMessageBoxView : Window
    {
        public StyledMessageBoxView()
        {
            this.ViewModel = new StyledMessageBoxViewModel();
            InitializeComponent();
        }

        public StyledMessageBoxViewModel ViewModel
        {
            get { return this.DataContext as StyledMessageBoxViewModel; }
            set { this.DataContext = value; }
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
