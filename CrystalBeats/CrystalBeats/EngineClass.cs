using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBeats
{
    public class Engine
    {
        Controller cController;
        Sequencer sSequencer;
        ProfileClass pProfile;
        public Engine()
        {
            cController = new Controller();
            sSequencer = new Sequencer();
            Sequence[] sequences = new Sequence[] {
                sSequencer.sqBar1,
                sSequencer.sqBar2,
                sSequencer.sqBar3,
                sSequencer.sqBar4,
                sSequencer.sqBar5,
                sSequencer.sqBar6,
                sSequencer.sqBar7,
                sSequencer.sqBar8};
            pProfile = new ProfileClass(sequences);
        }
    }
}
