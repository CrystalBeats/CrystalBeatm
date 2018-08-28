using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IrrKlang;
using System.ComponentModel;

namespace CrystalBeats
{
    class Sequencer
    {
        private int iBPM;

        public Sequence sqBar1, sqBar2, sqBar3, sqBar4, sqBar5, sqBar6, sqBar7, sqBar8;
        public Sequencer()
        {
            iBPM = 160;
            sqBar1 = new Sequence();
        }

        public void Play()
        {
            sqBar1.Start();
        }

        public void Stop()
        {
            sqBar1.Stop();
        }
    }

    class Sequence
    {
        private string strSoundslot;
        private int iDB;
        private int iBPM;
        private bool bRested;

        private int[] iPlayedBeats;

        private int iCounter;
        private int iBeatsPerSequence;
        private int iSequenceLength;

        private CrystalBeatmachine.Timer tTimer;

        private ISoundEngine sePlayer;

        public static readonly int[] iaBeatsPerSequenceEnum = { 2, 3, 4, 5, 6, 7,
                                                8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

        public static readonly int[] iaSequenceLengthEnum = { 4, 8, 16, 32 };

        public Sequence()
        {
            tTimer = new Timer();
            tTimer.Mode = TimerMode.Periodic;
            tTimer.Tick += new EventHandler(this.cbtTimer_Tick);

            iDB = 15;
            iBPM = 160;
            iCounter = 1;
            iBeatsPerSequence = 16;
            bRested = false;
            strSoundslot = @"C:\Users\chokemedaddy\Downloads\Berufsschule\adamnsampler-master\KORG WPF\Resources\acerBrandNeu14.wav";

            iPlayedBeats = new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };

            sePlayer = new ISoundEngine();
        }

        public Sequence(Profile SequencerProfile)
        {

        }

        private int getBPMasMS()
        {
            int MS = 60000 / iBPM;

            switch (iSequenceLength)
            {
                case 4:
                    break;
                case 8:
                    MS = MS / 2;
                    break;
                case 16:
                    MS = MS / 4;
                    break;
                case 32:
                    MS = MS / 8;
                    break;
            }
            return MS;
        }

        public void Start()
        {
            int setParams = getBPMasMS();

            tTimer.Period = setParams;
            tTimer.Resolution = setParams * iSequenceLength;

            if (iPlayedBeats == null || iPlayedBeats.Length != iBeatsPerSequence)//wenn die zu spielenden töne nicht belegt oder nicht des standards belegt sind
            {
                iPlayedBeats = new int[iBeatsPerSequence];
                iPlayedBeats[0] = 1;
                for (int i = 1; i < iBeatsPerSequence; i++)
                {
                    iPlayedBeats[i] = 0;                    //alle als nichtzuspielend markieren
                }
            }
            tTimer.Start();
        }

        public void Pause()
        {

        }

        public void Stop()
        {
            tTimer.Stop();
            iCounter = 1;
        }

        private void cbtTimer_Tick(object sender, EventArgs e)
        {
            if (iCounter > iBeatsPerSequence)
            {
                iCounter = 1;
            }

            if (iPlayedBeats[iCounter - 1] != 0 && !bRested)
            {
                sePlayer.Play2D(strSoundslot);
            }
            iCounter++;
        }

        public int BeatsPerSequence
        {
            get { return iBeatsPerSequence; }
            set
            {
                foreach (int i in iaBeatsPerSequenceEnum)
                {
                    if (value == i)
                    {
                        iBeatsPerSequence = value;
                        break;
                    }
                }
            }
        }

        public int SequenceLength
        {
            get { return iSequenceLength; }
            set
            {
                foreach (int i in iaSequenceLengthEnum)
                {
                    if (value == i)
                    {
                        iSequenceLength = value;
                        break;
                    }
                }
            }
        }

        public int DB
        {
            get { return iDB; }
            set
            {
                if (value > 0)
                {
                    iDB = value;
                    sePlayer.SoundVolume = value;
                }
            }
        }

        public int BPM
        {
            get { return iBPM; }
            set
            {
                if (value < 667 && value > 25)
                    iBPM = value;
            }
        }

        public int[] PlayedBeats
        {
            get { return iPlayedBeats; }
            set
            {
                if (value.Length <= 17)
                {
                    iPlayedBeats = value;
                }
            }
        }

        public bool Rested
        {
            get { return bRested; }
            set
            {
                bRested = value;
            }
        }

        public string Soundname
        {
            get { return strSoundslot; }
            set
            {
                if (value != "")
                {
                    strSoundslot = value;
                }
            }
        }
    }
}



