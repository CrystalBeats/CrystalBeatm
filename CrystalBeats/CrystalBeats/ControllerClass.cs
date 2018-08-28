using Microsoft.Win32;
using System.Collections.Generic;
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
        public Controller()
        {

        }

        public void turnSequence(Sequence seq)
        {
            seq.Rested = !seq.Rested;
        }

        public void enableVisuals(Sequence seq, List<Sequence> seqCol, Sequencer Sequencer)
        {
            foreach(Sequence seqDummy in seqCol)
            {
                seqDummy.Visible = (seqDummy == seq) ? true : false;
            }
            Sequencer.syncSequences();
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
    }
}
