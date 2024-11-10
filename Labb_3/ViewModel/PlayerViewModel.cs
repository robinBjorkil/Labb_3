using Labb_3.Command;
using Labb_3.Model;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Labb_3.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;
        private readonly ConfigurationViewModel? configurationViewModel;
        private readonly PlayerResultViewModel? playerResultViewModel;

        private Question? currentQuestion;
        private int currentQuestionIndex;

        private DispatcherTimer timer;
        private int remainingTime;
        public int RemainingTime
        {
            get => remainingTime;
            set
            {
                remainingTime = value;
                RaisePropertyChanged(nameof(RemainingTime));

            }
        }

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


            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            //timer.Start();
            //LoadFirstQuestion();


            AnswerCommand = new DelegateCommand(AnswerSelected);

        }

        public Question? CurrentQuestion
        {
            get => currentQuestion;
            set
            {
                currentQuestion = value;
                RaisePropertyChanged(nameof(CurrentQuestion));

            }
        }
        public ObservableCollection<Question> ShuffledQuestions = new ObservableCollection<Question>();
        public ObservableCollection<string> AnswerOptions { get; set; } = new ObservableCollection<string>();
        public DelegateCommand AnswerCommand { get; }


        private string _questionCounter;
        private int _rightAnswersNumber;

        public string QuestionCounter
        {

            get { return $"Fråga {currentQuestionIndex + 1} av {mainWindowViewModel.ActivePack.Questions.Count.ToString()}"; }
            set { _questionCounter = value; RaisePropertyChanged(); }
        }








        //      ***************************** METHODS *******************************
        public void LoadFirstQuestion()
        {
            if (mainWindowViewModel?.ActivePack?.Questions.Count > 0)
            {
                ShuffleQuestions();
                currentQuestionIndex = 0;
                RightAnswersNumber = 0;
                CurrentQuestion = ShuffledQuestions[currentQuestionIndex];
                RemainingTime = mainWindowViewModel!.ActivePack!.TimeLimitInSeconds;
                timer.Start();
                LoadAnswerOptions();
            }
        }

        private void ShuffleQuestions()
        {
            if (mainWindowViewModel?.ActivePack?.Questions != null)
            {
                var rnd = new Random();
                var tempQuestions = mainWindowViewModel.ActivePack.Questions.OrderBy(x => rnd.Next()).ToList();

                // Kopiera tillbaka till frågelistan
                // mainWindowViewModel.ActivePack.Questions.Clear();
                foreach (var question in tempQuestions)
                {
                    ShuffledQuestions.Add(question);
                }
            }
        }

        private void LoadAnswerOptions()
        {
            //AnswerOptions.Clear();

            if (CurrentQuestion != null)
            {

                var tempAnswerOptions = new ObservableCollection<String>();
                // Lägg till det korrekta svaret och de felaktiga svaren
                tempAnswerOptions.Add(CurrentQuestion.CorrectAnswer);

                foreach (var incorrectAnswer in CurrentQuestion.InCorrectAnswers)
                {

                    tempAnswerOptions.Add(incorrectAnswer);

                }
                AnswerOptions = tempAnswerOptions;
                // Blandar svarsalternativen
                ShuffleAnswers();
                //Debug.WriteLine("sssssssssssssssssssssssssssss" + AnswerOptions.Count);

            }
        }

        private void ShuffleAnswers()
        {
            var rnd = new Random();
            var shuffledAnswers = AnswerOptions.OrderBy(x => rnd.Next()).ToList();
            AnswerOptions = new ObservableCollection<string>(shuffledAnswers);

            //AnswerOptions.Clear();
            //foreach (var answer in shuffledAnswers)
            //{
            //    AnswerOptions.Add(answer);
            //}
            RaisePropertyChanged(nameof(AnswerOptions));
        }

        public int RightAnswersNumber
        { 
            get => _rightAnswersNumber;
            private set
            {
                _rightAnswersNumber = value;
                RaisePropertyChanged();
            
            }
        }
        private void AnswerSelected(object selectedAnswer)
        {
            if (selectedAnswer is string answer)
            {
                bool isCorrect = answer == CurrentQuestion?.CorrectAnswer;

                if (isCorrect)
                {
                    RightAnswersNumber++;

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
            //Debug.WriteLine("rrrrrrrrrrrrr" + currentQuestionIndex);
            if (++currentQuestionIndex < ShuffledQuestions.Count)
            {
                CurrentQuestion = ShuffledQuestions[currentQuestionIndex];
                RemainingTime = mainWindowViewModel.ActivePack.TimeLimitInSeconds; // Återställ tid
                LoadAnswerOptions();
                timer.Start(); // Starta om timern
            }
            else
            {
                if (IsPlayerViewVisible)
                {
                    timer.Stop();
                    IsPlayerViewVisible = false;
                    mainWindowViewModel.ConfigurationViewModel.IsMenuAndConfigVisible = false;
                    mainWindowViewModel.PlayerResultViewModel.IsPlayerResultViewVisible = true;
                }

            }
            RaisePropertyChanged(nameof(QuestionCounter));

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (RemainingTime > 0)
            {
                RemainingTime--; // Räkna ner tiden med 1 sekund
            }
            else
            {
                timer.Stop(); // Stoppa timern när tiden är slut
                NextQuestion();
            }

        }

    }
}
