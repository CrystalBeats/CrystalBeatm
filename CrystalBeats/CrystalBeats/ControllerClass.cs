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
        string strKey;
        Sequencer sqSequencer;
        ProfileClass pProfile;
        public Controller()
        {
            strKey = "";
            sqSequencer = new Sequencer();
            Sequence[] sequences = new Sequence[] {
                sqSequencer.sqSequences[0],
                sqSequencer.sqSequences[1],
                sqSequencer.sqSequences[2],
                sqSequencer.sqSequences[3],
                sqSequencer.sqSequences[4],
                sqSequencer.sqSequences[5],
                sqSequencer.sqSequences[6],
                sqSequencer.sqSequences[7]};
            pProfile = new ProfileClass(sequences);
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
            return (sqSequencer.aSequences[iSequence].PlayedBeats[iAccent] == 0) ? Brushes.Gray : Brushes.Purple;
        }

        public void setActiveBar(int iSequence)
        {
            sqSequencer.sqActiveSequence = sqSequencer.aSequences[iSequence];
        }

        public void PlayOnce()
        {
            sqSequencer.PlaySound();
        }

        public void sendCommand(string strCommand)
        {
            string asd = "";
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
                    break;
                case "2":
                    sqSequencer.aSequences[1].PlaySound();
                    break;
                case "3":
                    sqSequencer.aSequences[2].PlaySound();
                    break;
                case "4":
                    sqSequencer.aSequences[3].PlaySound();
                    break;
                case "5":
                    sqSequencer.aSequences[4].PlaySound();
                    break;
                case "6":
                    sqSequencer.aSequences[5].PlaySound();
                    break;
                case "7":
                    sqSequencer.aSequences[6].PlaySound();
                    break;
                case "8":
                    sqSequencer.aSequences[7].PlaySound();
                    break;
                default:
                    break;
            }
        }

    
    }
}
