﻿using Labb_3.Dialogs;
using Labb_3.ViewModel;
using Labb_3.Views;
using System.Windows;

namespace Labb_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();

        }
    }
}