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
        private ProfileClass pProfile;
        public Controller()
        {

            sqSequencer = new Sequencer();

            pProfile = new ProfileClass(sqSequencer.aSequences);

        }

        public void turnSequence(int iSequence)
        {
            sqSequencer.aSequences[iSequence].Rested = !sqSequencer.aSequences[iSequence].Rested;
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
                    break;
                case "2":
                    sqSequencer.aSequences[1].PlaySound();
                    sqSequencer.setActiveSequence(1);
                    break;
                case "3":
                    sqSequencer.aSequences[2].PlaySound();
                    sqSequencer.setActiveSequence(2);
                    break;
                case "4":
                    sqSequencer.aSequences[3].PlaySound();
                    sqSequencer.setActiveSequence(3);
                    break;
                case "5":
                    sqSequencer.aSequences[4].PlaySound();
                    sqSequencer.setActiveSequence(4);
                    break;
                case "6":
                    sqSequencer.aSequences[5].PlaySound();
                    sqSequencer.setActiveSequence(5);
                    break;
                case "7":
                    sqSequencer.aSequences[6].PlaySound();
                    sqSequencer.setActiveSequence(6);
                    break;
                case "8":
                    sqSequencer.aSequences[7].PlaySound();
                    sqSequencer.setActiveSequence(7);
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
