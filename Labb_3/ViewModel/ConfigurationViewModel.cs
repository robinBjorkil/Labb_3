 using Labb_3.Command;
using Labb_3.Dialogs;
using Labb_3.Model;
using Labb_3.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Labb_3.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;
        private readonly PlayerViewModel? playerViewModel;
        

        public QuestionPackViewModel? ActivePack { get => mainWindowViewModel?.ActivePack; }

        
        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            IsMenuAndConfigVisible = true;
            

            //************************ DELEGATE COMMANDS *****************************************

            //File Commands
            ImportQuestionsCommand = new DelegateCommand(ImportQuestions, CanImportQuestions);
            ExitCommand = new DelegateCommand(Exit, CanExit);
            
            // Edit and ConfigurationButton Commands
            PackOptionsCommand = new DelegateCommand(PackOptions, CanPackOptions);
            AddQuestionCommand = new DelegateCommand(AddQuestion, CanAddQuestion);
            RemoveQuestionCommand = new DelegateCommand(RemoveQuestion, CanRemoveQuestion);

            // View Commands
            
            
            FullScreenCommand = new DelegateCommand(FullScreen, CanFullScreen);

            
        }

        //************************ PROPERTIES ****************************************

        private bool _isMenuAndConfigVisible;
        //private bool _isPlayerViewVisible;
        //TEST AV VY

        public bool IsMenuAndConfigVisible
        {
            get => _isMenuAndConfigVisible;
            set
            {
                _isMenuAndConfigVisible = value;
                RaisePropertyChanged();
            }
        }

        // File Properties
        public DelegateCommand ImportQuestionsCommand { get; }
        public DelegateCommand ExitCommand { get; }      


        // Edit and ConfigurationButton Properties
        public DelegateCommand PackOptionsCommand { get; }
        public DelegateCommand AddQuestionCommand { get; }
        public DelegateCommand RemoveQuestionCommand { get; }


        // View Properties
       
        
        public DelegateCommand FullScreenCommand { get; }

        // GÖR TEXTBOXARNA OSYNLIGA
        private bool _isQuestionSelected;
        public bool IsQuestionSelected
        {
            get => _isQuestionSelected;
            set
            {
                _isQuestionSelected = value;
                RaisePropertyChanged();
            }
        }
        // GÖR TEXTBOXARNA OSYNLIGA

        // Select Question Field and Property
        private Question? _selectedQuestion;

        public Question? SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                if (_selectedQuestion != value)
                {
                    _selectedQuestion = value;
                    RaisePropertyChanged();

                    // GÖR TEXTBOXARNA OSYNLIGA
                    IsQuestionSelected = _selectedQuestion != null;
                    // GÖR TEXTBOXARNA OSYNLIGA

                    RemoveQuestionCommand.RaiseCanExecuteChanged();
                }
            }
        }

        //************************ METHODS ****************************************

        // *************** View Methods ***************
       

        

        private void FullScreen(object obj)
        {
            var mainWindow = App.Current.MainWindow;

            if (mainWindow.WindowState != WindowState.Maximized || mainWindow.WindowStyle != WindowStyle.None)
            {
                // Fullscreen
                mainWindow.WindowStyle = WindowStyle.None;
                mainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                // Reset to normal screen
                mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                mainWindow.WindowState = WindowState.Normal;
            }
        }

        private bool CanFullScreen(object? arg)
        {
            return true;
        }


        // *************** Edit and ConfigurationButton Methods ***************
        private void PackOptions(object obj)
        {
            var newPackOptionsDialog = new PackOptionsDialog();
            newPackOptionsDialog.ShowDialog();
        }

        private bool CanPackOptions(object? arg)
        {
            return true;
        }

        private void RemoveQuestion(object obj)
        {
            if (SelectedQuestion != null)
            {
                ActivePack?.Questions.Remove(SelectedQuestion);
                SelectedQuestion = null;
               
            }
        }

        private bool CanRemoveQuestion(object? obj) => SelectedQuestion != null;

        private bool CanAddQuestion(object? arg)
        {
            return true;
        }

        private void AddQuestion(object obj)
        {
            var newQuestion = new Question("New Question", "", "", "", "");
            ActivePack?.Questions.Add(newQuestion);
            SelectedQuestion = newQuestion;
            // _localDataService?.SaveQuestions(ActivePack?.Questions); (Json)
        }

        // *************** File Methods ***************
        private void Exit(object obj)
        {
            var mainWindow = App.Current.MainWindow;
            mainWindow?.Close();
        }

        private bool CanExit(object? arg)
        {
            return true;
        }

        private void ImportQuestions(object obj)
        {
            System.Windows.MessageBox.Show("Det är just nu inte möjligt att importera frågor men IT-teamet levererar strax en sådan funktion. Under tiden är det fullt möjligt att spara egna frågepaket med egna frågor.");
        }

        private bool CanImportQuestions(object? arg)
        {
            return true;
        }
        
    }
}


