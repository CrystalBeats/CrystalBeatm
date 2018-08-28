using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void enableVisuals(Sequence seq, List<Sequence> seqCol, Sequencer sequencer)
        {
            foreach(Sequence seqDummy in seqCol)
            {
                seqDummy.Visible = (seqDummy == seq) ? true : false;
            }
            sequencer.syncSequences;
        }

        public void turnAccent(int iAccent, int[] iAccentList)
        {
            iAccentList[iAccent] = (iAccentList[iAccent] == 0) ? 1 : 0;
        }

        public string setSound()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            return (openFileDialog.ShowDialog() == true) ? openFileDialog.FileName : "";
        }

        public void 


    }
}
