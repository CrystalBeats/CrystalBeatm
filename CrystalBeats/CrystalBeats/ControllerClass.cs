using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CrystalBeats
{
    public class Controller
    {

        public Sequencer sqSequencer;
        public ProfileClass pProfile;

        public string strSaveName;

        public Controller()
        {

            sqSequencer = new Sequencer();

            pProfile = new ProfileClass(sqSequencer.aSequences);

            this.strSaveName = String.Empty;
        }

        public void turnSequence(int iSequence)
        {
            sqSequencer.aSequences[iSequence].Rested = !sqSequencer.aSequences[iSequence].Rested;
        }

        public void loadProfile()
        {
            pProfile.loadProfile(OpenFileName());

            sqSequencer.aSequences = pProfile.pSequences;
        }

        public void saveProfile()
        {
            pProfile.pSequences = sqSequencer.aSequences;
            pProfile.saveProfile(SaveFileName(), this.strSaveName);
        }

        public void enableVisuals(Sequence seq, List<Sequence> seqCol, Sequencer Sequencer)
        {
            foreach (Sequence seqDummy in seqCol)
            {
                seqDummy.Visible = (seqDummy == seq) ? true : false;
            }
        }

        public void turnAccent(int iSequence, int iAccent)
        {
            sqSequencer.aSequences[iSequence].PlayedBeats[iAccent] = (sqSequencer.aSequences[iSequence].PlayedBeats[iAccent] == 0) ? 1 : 0;
        }

        public void setSoundFromFile(int iSequence)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            sqSequencer.aSequences[iSequence].Soundname = (openFileDialog.ShowDialog() == true) ? openFileDialog.FileName : "";
        }

        public string OpenFileName()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            return (openFileDialog.ShowDialog() == true) ? openFileDialog.FileName : "";
        }

        public string SaveFileName()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {

                this.strSaveName = saveFileDialog.FileName;
                return this.strSaveName;

            } else { return ""; }
                
        }

        public SolidColorBrush turnColor(int iSequence, int iAccent)
        {
            return (sqSequencer.aSequences[iSequence].PlayedBeats[iAccent] == 0) ? Brushes.Gray : Brushes.Purple ;
        }

        public void setActiveBar(int iSequence)
        {
            sqSequencer.sqActiveSequence = sqSequencer.aSequences[iSequence];
        }

        public void PlayOnce()
        {
            sqSequencer.PlaySound();
        }

        private void PauseBar()
        {
            sqSequencer.aSequences[iActiveBar].Rested = !sqSequencer.aSequences[iActiveBar].Rested;
        }

        private int iActiveBar
        {
            get
            {
                int x = 0;
                for (int i = 0; i<sqSequencer.aSequences.Length -1; i++)
                {
                    x = (sqSequencer.aSequences[i].Name == sqSequencer.ActiveSequence.Name) ? i : x;
                }

                return x;
            }
        }

        public void sendCommand(string strCommand)
        {
            switch(strCommand)
            {
                case ("Buttons6"): //HopScotch Feld wie in Array
                    sqSequencer.aSequences[0].PlaySound();
                    break;
                case "Buttons7":
                    sqSequencer.aSequences[1].PlaySound();
                    break;
                case "Buttons9":
                    sqSequencer.aSequences[2].PlaySound();
                    break;
                case "Buttons3":
                    sqSequencer.aSequences[3].PlaySound();
                    break;
                case "Buttons4":
                    sqSequencer.aSequences[4].PlaySound();
                    break;
                case "Buttons5":
                    sqSequencer.aSequences[5].PlaySound();
                    break;
                case "Buttons0":
                    sqSequencer.aSequences[6].PlaySound();
                    break;
                case "Buttons1":
                    sqSequencer.aSequences[7].PlaySound();
                    break;
                case ("1"): //HopScotch Feld wie in Array
                    sqSequencer.aSequences[0].PlaySound();
                    sqSequencer.setActiveSequence(0);
                    ((ViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ActivateSequenz((short)0, false);
                    break;
                case "2":
                    sqSequencer.aSequences[1].PlaySound();
                    sqSequencer.setActiveSequence(1);
                    ((ViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ActivateSequenz((short)1, false);
                    break;
                case "3":
                    sqSequencer.aSequences[2].PlaySound();
                    sqSequencer.setActiveSequence(2);
                    ((ViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ActivateSequenz((short)2, false);
                    break;
                case "4":
                    sqSequencer.aSequences[3].PlaySound();
                    sqSequencer.setActiveSequence(3);
                    ((ViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ActivateSequenz((short)3, false);
                    break;
                case "5":
                    sqSequencer.aSequences[4].PlaySound();
                    sqSequencer.setActiveSequence(4);
                    ((ViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ActivateSequenz((short)4, false);
                    break;
                case "6":
                    sqSequencer.aSequences[5].PlaySound();
                    sqSequencer.setActiveSequence(5);
                    ((ViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ActivateSequenz((short)5, false);
                    break;
                case "7":
                    sqSequencer.aSequences[6].PlaySound();
                    sqSequencer.setActiveSequence(6);
                    ((ViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ActivateSequenz((short)6, false);
                    break;
                case "8":
                    sqSequencer.aSequences[7].PlaySound();
                    sqSequencer.setActiveSequence(7);
                    ((ViewModel)((MainWindow)Application.Current.MainWindow).DataContext).ActivateSequenz((short)7, false);
                    break;
                case "p":
                    PauseBar();
                    break;
                default:
                    break;
            }
        }


    
    }
}
