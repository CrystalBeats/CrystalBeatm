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
    public class Sequencer
    {
        private int iBPM;

        public Sequence sqBar1, sqBar2, sqBar3, sqBar4, sqBar5, sqBar6, sqBar7, sqBar8;
        public Sequence[] sqSequences;
        public Sequence sqActiveSequence;

        public List<Sequence> colSequences;
        public Sequencer()
        {
            iBPM = 160;
            colSequences = new List<Sequence>();
            sqBar1 = new Sequence("bar1");
            sqBar2 = new Sequence("bar2");
            sqBar3 = new Sequence("bar3");
            sqBar4 = new Sequence("bar4");
            sqBar5 = new Sequence("bar5");
            sqBar6 = new Sequence("bar6");
            sqBar7 = new Sequence("bar7");
            sqBar8 = new Sequence("bar8");
            sqActiveSequence = new Sequence("");

            sqActiveSequence = sqBar1;

            colSequences.Add(sqBar1);
            colSequences.Add(sqBar2);
            colSequences.Add(sqBar3);
            colSequences.Add(sqBar4);
            colSequences.Add(sqBar5);
            colSequences.Add(sqBar6);
            colSequences.Add(sqBar7);
            colSequences.Add(sqBar8);


            // colSequences.CopyTo(sqSequences);

           // sqSequences = new 
          //  colSequences.CopyTo(sqSequences);
           // sqSequences = new 
           // colSequences.CopyTo(sqSequences);

        }

        public void Play()
        {
            sqBar1.Start();
        }

        public void Stop()
        {
            sqBar1.Stop();
        }

        public void syncSequences()
        {
  
            colSequences.CopyTo(sqSequences);
            sqBar1 = sqSequences[0];
            sqBar2 = sqSequences[1];
            sqBar3 = sqSequences[2];
            sqBar4 = sqSequences[3];
            sqBar5 = sqSequences[4];
            sqBar6 = sqSequences[5];
            sqBar7 = sqSequences[6];
            sqBar8 = sqSequences[7];
        }

        public void PlaySound()
        {
            sqActiveSequence.PlaySound();
        }

        public Sequence ActiveSequence
        {
            get { return sqActiveSequence; }
            set
            {
                sqActiveSequence = value;
            }
        }
    }

    public class Sequence
    {
        private string strSoundslot;
        private int iDB;
        private int iBPM;
        private bool bRested;
        private bool bVisible;

        private int[] iPlayedBeats;

        private int iCounter;
        private int iBeatsPerSequence;
        private int iSequenceLength;

        private readonly string strBarName;

        private CrystalBeats.Timer tTimer;

        private ISoundEngine sePlayer;

        public static readonly int[] iaBeatsPerSequenceEnum = { 2, 3, 4, 5, 6, 7,
                                                8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

        public static readonly int[] iaSequenceLengthEnum = { 4, 8, 16, 32 };

        public Sequence(string strBarname)
        {
            this.tTimer = new Timer();
            this.tTimer.Mode = TimerMode.Periodic;
            this.tTimer.Tick += new EventHandler(this.cbtTimer_Tick);

            this.iDB = 15;
            this.iBPM = 160;
            this.iCounter = 1;
            this.iBeatsPerSequence = 16;
            this.bRested = false;
            this.strBarName = strBarname;
            this.strSoundslot = @"C:\Users\chokemedaddy\Downloads\Berufsschule\adamnsampler-master\KORG WPF\Resources\acerBrandNeu14.wav";

            this.iPlayedBeats = new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 };

            this.sePlayer = new ISoundEngine();
        }

       // public Sequence(Profile SequencerProfile)
        //{

        //}

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
                sePlayer.StopAllSounds();
                sePlayer.Play2D(strSoundslot);
            }
            iCounter++;
        }

        public void PlaySound()
        {
            sePlayer.StopAllSounds();
            sePlayer.Play2D(strSoundslot);
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

        public bool Visible
        {
            get { return bVisible; }
            set
            {
                bVisible = value;
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

    public class Accent
    {
        private bool bPlayed;
        public Accent()
        {
            bPlayed = false;
        }

        public bool Played
        {
            get { return bPlayed; }
            set
            {
                bPlayed = !bPlayed;
            }
        }
    }
}



