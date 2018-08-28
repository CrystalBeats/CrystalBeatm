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
        public Engine()
        {
            cController = new Controller();
            sSequencer = new Sequencer();
        }
    }
}
