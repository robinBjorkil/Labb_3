using Labb_3.Dialogs;
using Labb_3.ViewModel;
using Labb_3.Views;
using System.Windows;

namespace Labb_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        //JSON
        private MainWindowViewModel _viewModel;
        //JSON

        public MainWindow()
        {
            InitializeComponent();

         

            DataContext = _viewModel = new MainWindowViewModel();

            //JSON
            Loaded += MainWindow_Loaded; // Händelsen för när fönstret laddas
            Closing += MainWindow_Closing;
            //JSON

        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //JSON
            await _viewModel.LoadDataAsync();
            //JSON
        }

        //JSON
        private async void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Spara data när applikationen stängs
            await _viewModel.SaveDataAsync(); // Sparar data
        }
        //JSON
    }
}