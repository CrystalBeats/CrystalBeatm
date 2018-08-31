using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
        ProfileClass profile;

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

        public ICommand SetBPM { get; set; }

        public ICommand SetDB { get; set; }

        public ICommand SetRecord { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand MinimizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

    //    public ICommand SetController { get; set; }

        public ViewModel()
        {
            // aktprofile 

            cController = new Controller();

            //todo implementierung

            PlayCommand = new RelayCommand(() => playSound());
            StopCommand = new RelayCommand(() => StopSound());

            MinimizeCommand = new RelayCommand(() => ((MainWindow)(Application.Current.MainWindow)).WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => ((MainWindow)(Application.Current.MainWindow)).WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => ((MainWindow)(Application.Current.MainWindow)).Close());

            SequenzKommand = new RelayParameterizedCommand((parameter) => ActivateSequenz(parameter));
            SelectSequenzKommand = new RelayParameterizedCommand(parameter => SetSequenz(parameter));
            NewProfile = new RelayCommand(() => neuesProfil());
            SaveProfile = new RelayCommand(() => speichereProfil());
            LoadProfile = new RelayCommand(() => ladeProfil());

      //      SelectSoundParameter = new RelayParameterizedCommand((parameter) => SetSpezificSequenz(parameter));
            SelectSchlag = new RelayParameterizedCommand(parameter => ActivateTurnAccent(parameter));
            SetBPM = new RelayCommand(() => SetBPMValue());

            SetDB = new RelayCommand(() => SetDBValue());

//            SetController = new RelayCommand(() => setControllerSource());

            int i = 0;

            ActivateSequenz((short)i);

            //((Label)((MainWindow)Application.Current.MainWindow).Panel_Schläge.Children[0]).Foreground = Brushes.Purple;
        }

        void SetDBValue()
        {
            string i = ((MainWindow)Application.Current.MainWindow).tb_Db.Text;

            int j = int.Parse(i);

            float db = j / 100f;

            cController.sqSequencer.aSequences[this.Sequenz].DB = db;

        }

        void playSound()
        {
            cController.sqSequencer.Play();

            this.AktTitle = Path.GetFileName(cController.sqSequencer.ActiveSequence.Soundname);
        }

        void StopSound()
        {
            cController.sqSequencer.Stop();
            this.AktTitle = "STOPED";
        }

        void SetBPMValue()
        {
            string i = ((MainWindow)Application.Current.MainWindow).tb_BPM.Text;
            cController.sqSequencer.BPM = int.Parse(i);


            this.AktBPM = "Aktuelle BPM: " + cController.sqSequencer.BPM.ToString();
        }

        void ActivateTurnAccent(object parameter)
        {

            var i = (short)parameter;

            this.Schlag = (int)i;

            cController.turnAccent(this.Sequenz, this.Schlag);
            SetzeButtonBackground(this.Schlag);


        }

        public void SetSequenz(object parameter, bool setFile = true)
        {

            var i = (short)parameter;

            this.Sequenz = (int)i;

            if (setFile) { 
            cController.setSoundFromFile(this.Sequenz);
            } 

            
        }
        
        public void neuesProfil()
        {
       //     profile = new ProfileClass();

            cController.new

        }

        public void ladeProfil()
        {
            //// Ort von Profil
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Multiselect = false;
            //openFileDialog.Filter = "*.xml, *.*";
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    aktprofile.loadProfile(openFileDialog.FileName);
            //}

            cController.ladeProfil();



        }

        public void speichereProfil()
        {

            cController.saveProfile();

        }

        void ActivateSequenz(object parameter)
        {

            SetSequenz(parameter, false);

            cController.sqSequencer.setActiveSequence(this.Sequenz);
    

            SetzeButtonsSequenz(this.Sequenz);

            this.AktTitle = Path.GetFileName(cController.sqSequencer.ActiveSequence.Soundname);
            this.AktBPM = "Aktuelle BPM: " + cController.sqSequencer.BPM.ToString();


        }


       public void ActivateSequenz(object parameter, bool ActiveSequenz = true)
        {

            

            SetSequenz(parameter, false);

            if (ActiveSequenz) { 
            cController.sqSequencer.setActiveSequence(this.Sequenz);
            }

            SetzeButtonsSequenz(this.Sequenz);

            this.AktTitle = Path.GetFileName(cController.sqSequencer.ActiveSequence.Soundname);
            this.AktBPM = "Aktuelle BPM: " + cController.sqSequencer.BPM.ToString();

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

        private string mNameProfil = "Aktuelles Profil: Neues Profil";

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


        //Ansteuerung SettingsBereich

        //Ansteuerung Steuerbereich



        //Ansteuerung Bars/Sequenzen

        //Ansteuerung Schläge

        #endregion

        //
        #region Controller

        private int mSequenz = 0;

        public int Sequenz
        {
            get { return mSequenz; }
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

            foreach (Button button in ((MainWindow)Application.Current.MainWindow).border_Sequenz.Children.OfType<Button>())
            {
                if ((short)button.CommandParameter == SequenzNr)
                {
                    button.Background = Brushes.Purple;

                   
                    foreach (Button beatbutton in ((MainWindow)Application.Current.MainWindow).Border_Buttons.Children.OfType<Button>())
                    {
                        beatbutton.Background = cController.turnColor(SequenzNr, (int)(short)beatbutton.CommandParameter);
                    }



                        continue;
                }
                else
                {
                    if (button.Background != Brushes.Gray)
                    {
                        button.Background = Brushes.Gray;
                    }
                }
            }
           
            
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



        //void setSchlagInDisplay()
        //{
        //    cController.sqSequencer.ActiveSequence.ActualBeat
        //}

        #endregion
    }
}
