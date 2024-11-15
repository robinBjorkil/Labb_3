using Labb_3.Model;
using System.Collections.ObjectModel;


namespace Labb_3.ViewModel
{
    class QuestionPackViewModel : ViewModelBase
    {
        private readonly QuestionPack model;

        public QuestionPackViewModel(QuestionPack model)
        {
            this.model = model;
            this.Questions = new ObservableCollection<Question>(model.Questions);
        }

        public string Name
        {
            get => model.Name;

            set
            {
                model.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public Difficulty Difficulty
        {
            get => model.Difficulty;

            set
            {
                model.Difficulty = value;
                RaisePropertyChanged(nameof(Difficulty));
            }
        }

        public int TimeLimitInSeconds
        {
            get => model.TimeLimitInSeconds;

            set
            {
                model.TimeLimitInSeconds = value;
                RaisePropertyChanged(nameof(TimeLimitInSeconds));
            }
        }

        public ObservableCollection<Question> Questions { get; }


        //JSON
        public QuestionPack ConvertToQuestionPack()
        {
            QuestionPack pack = new QuestionPack(this.Name, this.Difficulty, this.TimeLimitInSeconds);
            foreach (var question in this.Questions)
            {
                pack.Questions.Add(new Question(
                    question.Query, 
                    question.CorrectAnswer,
                    question.InCorrectAnswers[0],
                    question.InCorrectAnswers[1],
                    question.InCorrectAnswers[2]
                    ));
            }
            return pack;

            
        }
        //JSON
        

        public override string ToString()
        {
            return $"{Name} ({Difficulty}) - {TimeLimitInSeconds} Seconds";
        }


    }
}
