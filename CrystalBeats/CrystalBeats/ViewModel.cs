using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrystalBeats
{
    class ViewModel: INotifyPropertyChanged
    {

        Sequencer sequencer;

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void onPropertyChanged(object propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(propName)));
        }

        #endregion

        #region RelayCommand
        public class RelayCommand : ICommand
        {

            private Action mAction;

            public RelayCommand(Action action)
            {
                mAction = action;
            }

            public event EventHandler CanExecuteChanged = (sender, e) => { };

            public bool CanExecute(object parameter)
            {
                return true;
            }


            public void Execute(object parameter)
            {
                mAction();
            }
        }

        #endregion

        public ICommand PlayCommand { get; set; }

        public ViewModel()
        {
            sequencer = new Sequencer();
            PlayCommand = new RelayCommand(() => sequencer.Play());
        }


        
    }
}
