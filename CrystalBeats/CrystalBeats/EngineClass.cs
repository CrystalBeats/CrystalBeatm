using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBeats
{
    public class Engine
    {
<<<<<<< HEAD
        ProfileClass pProfile;

        public Controller cController;
        public Sequencer sSequencer;
<<<<<<< HEAD
=======

        ProfileClass pProfile;
=======
>>>>>>> a8291945fe3b33f0e27d3d74fe1df834c34ab6fc

        public Controller cController;
        public Sequencer sSequencer;

<<<<<<< HEAD
>>>>>>> Frontend
=======
>>>>>>> a8291945fe3b33f0e27d3d74fe1df834c34ab6fc
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
