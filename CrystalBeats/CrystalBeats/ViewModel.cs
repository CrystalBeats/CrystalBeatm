using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CrystalBeats
{
   public class ViewModel: INotifyPropertyChanged
    {

        //Frontend: 
        //

        #region Properties für Backend - Stuff
        public Controller cController;

        #endregion

        //Einfache PropertyChangedImplementation
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void onPropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
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

        #region RelayParameterizedCommand
        /// <summary>
        /// A basic command that runs an Action
        /// </summary>
        public class RelayParameterizedCommand : ICommand
        {
            #region Private Members

            /// <summary>
            /// The action to run
            /// </summary>
            private Action<object> mAction;

            #endregion

            #region Public Events

            /// <summary>
            /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
            /// </summary>
            public event EventHandler CanExecuteChanged = (sender, e) => { };

            #endregion

            #region Constructor

            /// <summary>
            /// Default constructor
            /// </summary>
            public RelayParameterizedCommand(Action<object> action)
            {
                mAction = action;
            }

            #endregion

            #region Command Methods

            /// <summary>
            /// A relay command can always execute
            /// </summary>
            /// <param name="parameter"></param>
            /// <returns></returns>
            public bool CanExecute(object parameter)
            {
                return true;
            }

            /// <summary>
            /// Executes the commands Action
            /// </summary>
            /// <param name="parameter"></param>
            public void Execute(object parameter)
            {
                mAction(parameter);
            }

            #endregion
        }

        #endregion

        //TODO: Schnittstellen zu Backend fertig stellen
        #region Commands und Konstruktor
        public ICommand PlayCommand { get; set; }

        public ICommand StopCommand { get; set; }
        public ICommand SequenzKommand { get; set; }

        public ICommand SelectSequenzKommand { get; set; }

        public ICommand NewProfile { get; set; }
        public ICommand SaveProfile { get; set; }
        public ICommand LoadProfile { get; set; }

        public ICommand SelectSoundParameter { get; set; }

        public ICommand SelectSchlag { get; set; }

        public ViewModel()
        {
            // aktprofile 

            cController = new Controller();

            //todo implementierung

            PlayCommand = new RelayCommand(() => cController.sqSequencer.Play());
            StopCommand = new RelayCommand(() => cController.sqSequencer.Stop());

            SequenzKommand = new RelayCommand(() => ActivateSequenz());
            SelectSequenzKommand = new RelayParameterizedCommand(parameter => SetSequenz(parameter));
            //NewProfile = new RelayCommand(() => neu esProfil());
            //SaveProfile = new RelayCommand(() => speichereProfil());
            //LoadProfile = new RelayCommand(() => ladeProfil());

      //      SelectSoundParameter = new RelayParameterizedCommand((parameter) => SetSpezificSequenz(parameter));
            SelectSchlag = new RelayParameterizedCommand(parameter => ActivateTurnAccent(parameter));
        }

        void ActivateTurnAccent(object parameter)
        {

            var i = (short)parameter;

            this.Schlag = (int)i;

            cController.turnAccent(this.Sequenz, this.Schlag);
        }

        public void SetSequenz(object parameter)
        {

            var i = (short)parameter;

            this.Sequenz = (int)i;

            cController.setSoundFromFile(this.Sequenz);
        }
        
        public void ladeProfil()
        {
            //// Ort von Profil
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Multiselect = false;
            //openFileDialog.Filter = "*.xml, *.*";
            //if (openFileDialog.ShowDialog() == true) { 
            //aktprofile.loadProfile(openFileDialog.FileName);
            //}

            //controller.ladeProfil();

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

        void ActivateSequenz()
        {

            cController.sqSequencer.setActiveSequence(this.Sequenz);

            SetzeButtonsSequenz(this.Sequenz);
        }

    
      
        
        public void SprecheAktiveSequenzAn()
        {
            //Array
            //sequencer.ActiveSequence.PlayedBeats
            
        }

        public void SetSpezificSequenz(object parameter)
        {
      

            this.Sequenz = (int)parameter;

            
            
        }

        public async Task SetSpezificSchlag(object parameter)
        {
            Debugger.Break();
            this.Schlag = (int)parameter;

         //   await Task.Run(SelectSchlag);

         //  controller.turnAccent(this.Sequenz, this.Schlag);
        }

        #endregion

        #region Ansteuerung Frontend

        //Ansteuerung Display
        #region Display
        private string mAktBMP;
    
        public string AktBPM
        {
            get { return mAktBMP; }
            set
            {
                mAktBMP = value;
              //  mAktBMP = setBPM();
                onPropertyChanged("AktBPM");
            }
        }

        private string mNameProfil;

        public string NameProfil
        {
            get { return mNameProfil; }
            set { mNameProfil = value; onPropertyChanged("NameProfil"); }
        }

        private string mAktTitle ;

        public string AktTitle
        {
            get { return mAktTitle; }
            set
            {
                
                mAktTitle = value;
                onPropertyChanged("AktTitle");
            }

        }


        private string mTime;

        public string Time
        {
            get { return mTime; }
            set { mTime = value; onPropertyChanged("Time"); }
        }

        private int mAktIndex;

        public int aktIndex
        {
            get { return mAktIndex; }
            set { mAktIndex = value; onPropertyChanged("aktIndex"); }
        }

        void Stuff()
        {

            #region Controller
            //Input: BeatArray + Position, wenn Null Farbe Grau ansonsten Farbe Lila
            //controller.enableVisuals

            //Einmaliges Abspielen der Sequenz
            //controller.PlayOnce


            //Sequenz & Sequenzer
            //controller.setActiveBar

            //OpenFileDialog
            //controller.setSoundFromFile

            //Ändert Schlag
            //controller.turnAccent

            //Ändert Farbe Schlag
            //controller.turnColor

            //Ändere Sequenz
            //controller.turnSequence
            #endregion

            
        }


   



        //Ansteuerung SettingsBereich

        //Ansteuerung Steuerbereich



        //Ansteuerung Bars/Sequenzen

        //Ansteuerung Schläge

        #endregion

        //
        #region Controller

        private int mSequenz;

        public int Sequenz
        {
            get { SetzeButtonsSequenz(mSequenz); return mSequenz; }
            set { mSequenz = value; onPropertyChanged("Sequenz");  }
        }

        private int mSchlag;

        public int Schlag
        {
            get { return mSchlag; }
            set { mSchlag = value; onPropertyChanged("Schlag"); SetzeButtonBackground(value); }
        }


        #endregion

        void SetzeButtonsSequenz(int SequenzNr)
        {

            Debugger.Break();
           
            
        }

        void SetzeButtonBackground(object value)
        {

            var i = (int)value;
            var compare = (short)i;

            foreach (Button button in ((MainWindow)Application.Current.MainWindow).Border_Buttons.Children.OfType<Button>())
            {

                if ((short)button.CommandParameter == compare)
                {
                    button.Background = cController.turnColor(this.Sequenz, this.Schlag);
                    break;
                }


            }
        }

        #endregion
    }
}
