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

        public void turnSequence(Sequence seq)
        {
            seq.Rested = !seq.Rested;
        }

        public void enableVisuals(Sequence seq, List<Sequence> seqCol, Sequencer Sequencer)
        {
            foreach (Sequence seqDummy in seqCol)
            {
                seqDummy.Visible = (seqDummy == seq) ? true : false;
            }
        }

        public void turnAccent(int iAccent, int[] iAccentList)
        {
            iAccentList[iAccent] = (iAccentList[iAccent] == 0) ? 1 : 0;
        }

        public string setSoundFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            return (openFileDialog.ShowDialog() == true) ? openFileDialog.FileName : "";
        }

        public SolidColorBrush turnColor(int[] Accents, int iAccent)
        {
            return (Accents[iAccent] == 0) ? Brushes.Gray : Brushes.Purple;
        }

        public void setActiveBar(Sequence sequence, Sequencer sequencer)
        {
            sequencer.sqActiveSequence = sequence;
        }

        public void PlayOnce(Sequencer sequencer)
        {
            sequencer.PlaySound();
        }

        public void sendCommand(string strCommand)
        {
            string asd = "";
            switch(strCommand)
            {
                case "Buttons6": //HopScotch Feld wie in Array
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
                default:
                    break;
            }
        }

    
    }
}
