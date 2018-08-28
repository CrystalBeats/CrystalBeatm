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

        public void enableVisuals(Sequence seq, List<Sequence> seqCol)
        {
            foreach(Sequence seqDummy in seqCol)
            {
                seqDummy.Visible = (seqDummy == seq) ? true : false;
            }
        }

        public void turnAccent(int iAccent, int[] iAccentList)
        {
            iAccentList[iAccent] = (iAccentList[iAccent] == 0) ? 1 : 0;
        }

        public void playSound()
        {

        }

        public void 


    }
}
