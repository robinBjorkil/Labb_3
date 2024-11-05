using Labb_3.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Labb_3.ViewModel 
{
    internal class PlayerResultViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private bool _isPlayerResultViewVisible;
        public bool IsPlayerResultViewVisible
        {
            get => _isPlayerResultViewVisible;
            set
            {
                _isPlayerResultViewVisible = value;
                RaisePropertyChanged("IsPlayerResultViewVisible");

            }
        }

        public DelegateCommand RestartCommand { get; }

        public PlayerResultViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            IsPlayerResultViewVisible = false;

            RestartCommand = new DelegateCommand(Restart);

        }

        private void Restart(object obj)
        {
            //Vad händer när jag trycker på restartknappen?
            throw new NotImplementedException();
        }
    }
}
