using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrystalBeats
{
    class ViewModel: INotifyPropertyChanged
    {

        Sequencer sequencer;
        Controller controller;
        ProfileClass aktprofile;

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
        public ICommand SelectSound { get; set; }

        public ICommand NewProfile { get; set; }
        public ICommand SaveProfile { get; set; }
        public ICommand LoadProfile { get; set; }

        public ViewModel()
        {

            sequencer = new Sequencer();
            controller = new Controller();
            

            PlayCommand = new RelayCommand(() => sequencer.Play());
            SelectSound = new RelayCommand(() => SetController());
            NewProfile = new RelayCommand(() => neuesProfil());
            SaveProfile = new RelayCommand(() => speichereProfil());
            LoadProfile = new RelayCommand(() => ladeProfil());
        }

        public void neuesProfil()
        {
            // Todo neues Profil sollte nur in EngineClass angelegt werden
            Sequence[] sequences = new Sequence[] {
                sequencer.sqBar1,
                sequencer.sqBar2,
                sequencer.sqBar3,
                sequencer.sqBar4,
                sequencer.sqBar5,
                sequencer.sqBar6,
                sequencer.sqBar7,
                sequencer.sqBar8};
            aktprofile = new ProfileClass(sequences);
        }

        public void ladeProfil()
        {
            // Ort von Profil
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "*.xml, *.*";
            if (openFileDialog.ShowDialog() == true) { 
            aktprofile.loadProfile(openFileDialog.FileName);
            }

        }

        public void speichereProfil()
        {
            // Name für Profil und Ort für Profil
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string name;

            InputBox akt_input = new InputBox();

            akt_input.ShowDialog();

            if (akt_input.DialogResult == true)
            {
                name = akt_input.inputString;
            }

            Debugger.Break();

            //string profilename = Microsoft.VisualBasic.Interaction.InputBox("Prompt", "Title", "Default", -1, -1);

            //if (.Show() == true)
            //{
            //
            //}
            //
            //if (saveFileDialog.ShowDialog() == true)
            //aktprofile.saveProfile();
        }

        public void SetController()
        {
            //if (sequencer.sqBar1.Soundname == String.Empty)
           // { 
            sequencer.sqBar1.Soundname = controller.setSoundFromFile();
           // }

            sequencer.Play();

        }
        
    }
}
