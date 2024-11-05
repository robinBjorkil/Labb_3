using Labb_3.Views;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Labb_3.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

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

            // Exempel på hur jag kan använda Command för att uppdatera olika saker.
            // Obs! - read only properties ovan för exemplen.
            //UpdateButtonCommand = new DelegateCommand(updateButton CanUpdateButton);
            //AddQuestionCommand = new DelegateCommand(AddQuestion, CanAddQuestion);
        }

        
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
