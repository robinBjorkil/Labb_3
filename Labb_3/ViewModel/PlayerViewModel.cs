using Labb_3.Command;
using Labb_3.Model;
using Labb_3.Views;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Labb_3.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;


        //TEST PLAYERBINDING ********************************************
       
        private Question? currentQuestion;
        private int currentQuestionIndex;
        //TEST PLAYERBINDING ********************************************


        private DispatcherTimer timer;

        ////TIMER
        //private int remainingTime;


        //public int RemainingTime
        //{
        //    get => remainingTime;
        //    set
        //    { 
        //        remainingTime = value;


        //    }
        //}
        ////TIMER


        private bool _isPlayerViewVisible;
        public bool IsPlayerViewVisible
        {
            get => _isPlayerViewVisible;
            set
            {
                _isPlayerViewVisible = value;
                RaisePropertyChanged("IsPlayerViewVisible");
               
            }
        }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            IsPlayerViewVisible = false;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            //timer.Start();

            //TEST PLAYERBINDING ***************************************************
            LoadFirstQuestion();
            //TEST PLAYERBINDING ***************************************************
            AnswerCommand = new DelegateCommand(AnswerSelected);
        }

       

        //TEST PLAYERBINDING ************************************************************
        public Question? CurrentQuestion
        {
            get => currentQuestion;
            set
            {
                currentQuestion = value;
                RaisePropertyChanged(nameof(CurrentQuestion));
            }
        }

        public ObservableCollection<string> AnswerOptions { get; set; } = new ObservableCollection<string>();
        public DelegateCommand AnswerCommand { get; }

        

        public void LoadFirstQuestion()
        {
            if (mainWindowViewModel?.ActivePack?.Questions.Count > 0)
            {
                currentQuestionIndex = 0;
                CurrentQuestion = mainWindowViewModel.ActivePack.Questions[currentQuestionIndex];
                LoadAnswerOptions();
            }
        }

        private void LoadAnswerOptions()
        {
            AnswerOptions.Clear();
            if (CurrentQuestion != null)
            {
                // Lägg till det korrekta svaret och de felaktiga svaren
                AnswerOptions.Add(CurrentQuestion.CorrectAnswer);
                foreach (var incorrectAnswer in CurrentQuestion.InCorrectAnswers)
                {
                    AnswerOptions.Add(incorrectAnswer);
                }
                ShuffleAnswers();  // Blandar svarsalternativen
            }
        }

        private void ShuffleAnswers()
        {
            var rnd = new Random();
            AnswerOptions = new ObservableCollection<string>(AnswerOptions.OrderBy(x => rnd.Next()));
        }

        private void AnswerSelected(object selectedAnswer)
        {
            if (selectedAnswer is string answer)
            {
                bool isCorrect = answer == CurrentQuestion?.CorrectAnswer;

                if (isCorrect)
                {
                    // Markera knappen som grön (korrekt)
                }
                else
                {
                    // Markera vald knapp som röd och visa rätt svar
                }

                NextQuestion();
            }
        }
        

        private void NextQuestion()
        {
            if (++currentQuestionIndex < mainWindowViewModel?.ActivePack?.Questions.Count)
            {
                CurrentQuestion = mainWindowViewModel.ActivePack.Questions[currentQuestionIndex];
                LoadAnswerOptions();
            }
            else
            {
                // Hantera avslut av quiz
            }
        }
        //TEST PLAYERBINDING ************************************************************

        private void Timer_Tick(object? sender, EventArgs e)
        {
            ////TIMER
            //if (RemainingTime > 0)
            //{
            //    RemainingTime--; // Räkna ner tiden med 1 sekund
            //}
            //else
            //{
            //    timer.Stop(); // Stoppa timern när tiden är slut
            //    // Vad händer när tiden tar slut?
            //}
            ////TIMER
        }
        ////TIMER
        //public void StartTimer(int startValue)
        //{
        //    RemainingTime = startValue;
        //    timer.Start();
        //}
        ////TIMER
    }
}
