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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TypingApp.Views
{
    /// <summary>
    /// Interaction logic for GroupView.xaml
    /// </summary>
    public partial class TeacherDashboardView : UserControl
    {
        public TeacherDashboardView()
        {
            InitializeComponent();
        }

        private void GoRight(object sender, RoutedEventArgs e)
        {
            ScrollViewer.PageRight();
        }

        private void GoLeft(object sender, RoutedEventArgs e)
        {
            ScrollViewer.PageLeft();
        }
    }
}
