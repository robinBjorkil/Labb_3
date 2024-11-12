using Labb_3.Command;
using Labb_3.Dialogs;
using Labb_3.Model;
using Labb_3.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Labb_3.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
	{
        

        private CreateNewPackDialog? _newPackDialog;

        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }


        public QuestionPackViewModel? NewPack { get; set; }
        public ConfigurationViewModel ConfigurationViewModel { get; }

        public PlayerViewModel PlayerViewModel { get; }

        public PlayerResultViewModel PlayerResultViewModel { get; }

        public DelegateCommand EditCommand { get; }

        private QuestionPackViewModel? _activePack;

		public QuestionPackViewModel?  ActivePack
		{
			get => _activePack;
			set
			{
				_activePack = value;
				RaisePropertyChanged();

                ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            }
        }

        public MainWindowViewModel()
        {
            //IsMenuAndConfigVisible = true;
            //IsPlayerViewVisible = false;
            
            Packs = new ObservableCollection<QuestionPackViewModel>();
            //Json.Load();

			ConfigurationViewModel = new ConfigurationViewModel(this);


            EditCommand = new DelegateCommand(Edit, CanEdit);


            PlayerViewModel = new PlayerViewModel(this);

            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));

			Packs.Add(ActivePack);

            PlayerViewModel.AnswerOptions.Add("");
            PlayerViewModel.AnswerOptions.Add("");
            PlayerViewModel.AnswerOptions.Add("");
            PlayerViewModel.AnswerOptions.Add("");

            //PlayerViewModel.ButtonContent.Add("");
            //PlayerViewModel.ButtonContent.Add("");
            //PlayerViewModel.ButtonContent.Add("");
            //PlayerViewModel.ButtonContent.Add("");



            PlayerResultViewModel = new PlayerResultViewModel(this);



            // *************** DELEGATE COMMANDS **********************
            ConfirmAndCreateQuestionPackCommand = new DelegateCommand(CreateQuestionPack);
            AddQuestionPackCommand = new DelegateCommand(AddQuestionPack, CanAddQuestionPack);
            SelectQuestionPackCommand = new DelegateCommand(SelectQuestionPack);
            CancelQuestionPackCommand = new DelegateCommand(CancelQuestionPack);
            DeleteQuestionPackCommand = new DelegateCommand(DeleteQuestionPack, CanDeleteQuestionPack);
            PlayCommand = new DelegateCommand(Play);
            
        }

        


        // ************** PROPERTIES **************************
        public DelegateCommand DeleteQuestionPackCommand { get; }
        public DelegateCommand SelectQuestionPackCommand { get; }
        public DelegateCommand CancelQuestionPackCommand { get; }
        public DelegateCommand ConfirmAndCreateQuestionPackCommand { get; }
        public DelegateCommand AddQuestionPackCommand { get; }
        public DelegateCommand PlayCommand { get; }


        // ****************** METHODS **************************
        public void Play(object obj)
        { 

            ConfigurationViewModel.IsMenuAndConfigVisible = false; // Dölj menyn och config

            PlayerViewModel.IsPlayerViewVisible = true;
            PlayerViewModel.ShuffledQuestions.Clear();
            PlayerViewModel.LoadFirstQuestion();
            
            PlayerViewModel.RaisePropertyChanged(nameof(PlayerViewModel.QuestionCounter));

        }

        
        private void CancelQuestionPack(object obj)
        {
            _newPackDialog!.Close();
        }

        private void SelectQuestionPack(object obj)
        {
            if (obj is QuestionPackViewModel selectedPack)
            {
                ActivePack = selectedPack;
                
                //System.Windows.MessageBox.Show($"Selected {ActivePack}");
                
            }
        }

        private void Edit(object obj)
        {
            ConfigurationViewModel.IsMenuAndConfigVisible = true; // Dölj menyn och config
            PlayerViewModel.IsPlayerViewVisible = false;
            PlayerResultViewModel.IsPlayerResultViewVisible = false;
        }

        private bool CanEdit(object? arg)
        {
            return true;
        }

        private void CreateQuestionPack(object obj)
        {

            Packs.Add(NewPack!);
            ActivePack = NewPack;
            _newPackDialog!.Close();

        }

        public void AddQuestionPack(object obj)
        {
            NewPack = new QuestionPackViewModel(new QuestionPack("New Pack"));
       
            _newPackDialog = new CreateNewPackDialog();
            _newPackDialog.ShowDialog();

        }

        public bool CanAddQuestionPack(object? arg)
        {
            // Logik för att avgöra om det ska gå att lägga till ett nytt QuestionPack eller inte.
            return true;
        }

        private void DeleteQuestionPack(object obj)
        {

            if (ActivePack != null)
            {
                var Result = MessageBox.Show($"Do you want to delete: '{ActivePack}'?", $"Delete: '{ActivePack}'?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    MessageBox.Show($"Deleted {ActivePack}");
                    Packs.Remove(ActivePack);

                    if (Packs.Count > 0)
                    {
                        ActivePack = Packs[^1]; // Sätter ActivePack till det sista objektet i listan
                        SelectQuestionPack(ActivePack);
                    }
                    else
                    {
                        ActivePack = null; // Om listan är tom
                    }

                }
                else if (Result == MessageBoxResult.No)
                {
                    return;
                }
            }
        }

        private bool CanDeleteQuestionPack(object? arg)
        {
            return true;
        }

    }
}
