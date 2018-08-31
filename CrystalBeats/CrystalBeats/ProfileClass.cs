using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpConfig;


namespace CrystalBeats
{
    public class ProfileClass
    {
        private Sequence[] bars = new Sequence[8];
        public string mprofilename;
        // 8 bars x 16 beats
        int[,] accentedBeats = new int[,] {
            {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        // Temporärer Pfad
        string presetprofile = @"..\..\..\Profiles\presetprofile.ini";
        Configuration config = new Configuration();
        
        // Constructor
        public ProfileClass(Sequence[] sequences)
        {
            Console.WriteLine(presetprofile);
            for (int i = 0; i < sequences.Length; i++)
            {
                bars[i] = sequences[i];
            }

            // Speicherbeispiel
            /*string saveto = @"..\..\..\Profiles\tosave.ini";
            saveProfile(saveto);*/
        }
        // Getter setter accentedbeats
        public int[] getAccentedBeats(int bar) { return bars[bar].PlayedBeats; }
        public void setAccentedBeats(SharpConfig.Section[] sections)
        {
            // Loop bars
            for (int i = 0; i < sections.Length; i++)
            {
                int[] loadingbeats = sections[i]["accents"].IntValueArray;
                // Loop beats
                for(int a = 0; a < loadingbeats.Length; a++)
                {
                    accentedBeats[i,a] = loadingbeats[a];
                }
            }
        }
        public int getAccentedBeat(int bar, int accentedbeat)
        {
            int[] beats = bars[bar].PlayedBeats;
            return beats[accentedbeat];
            //return accentedBeats[bar, accentedbeat];
        }
        public void setAccentedBeat(int bar, int accentedbeat, int val)
        {
            accentedBeats[bar, accentedbeat] = val;
        }

        // Getter Setter Bar
        public Sequence getBar(int index){return bars[index];}

        public void loadProfile(string profile)
        {
            config = Configuration.LoadFromFile(profile);
            mprofilename = config["General"]["name"].StringValue;
            var sectionG = config["General"];
            SharpConfig.Section[] sections = new SharpConfig.Section[] {
                config["Korg1"],
                config["Korg2"],
                config["Korg3"],
                config["Korg4"],
                config["Korg5"],
                config["Korg6"],
                config["Korg7"],
                config["Korg8"]
            };

            //Set profile values for bars
            int inibpm = Int32.Parse(sectionG["bpm"].StringValue);
            setBpms(inibpm);
            setSounds(sections);
            setVolumes(sections);
            setRests(sections);
            setAccentedBeats(sections);

        }
        public void saveProfile(string filetosave, string profilename)
        {
            config = Configuration.LoadFromFile(presetprofile);
            config["General"]["name"].StringValue = profilename;
            config["General"]["bpm"].IntValue = bars[1].BPM;
            // Save Bars
            for(int i = 0; i < bars.Length; i++)
            {
                int korgname = i + 1;
                config["Korg" + korgname]["soundslot"].StringValue = bars[i].Soundname;
                config["Korg" + korgname]["db"].FloatValue = bars[i].DB;
                config["Korg" + korgname]["rest"].BoolValue = bars[i].Rested;
                int[] savingbeats = getAccentedBeats(i);
                config["Korg" + i]["accents"].IntValueArray = savingbeats;
            }

            config.SaveToFile(filetosave);
        }

        public void setBpms(int val)
        {
            for (int i = 0; i < bars.Length; i++)
            {
                bars[i].BPM = val;
            }
        }
        public void setSounds(SharpConfig.Section[] sections)
        {
            for (int i = 0; i < bars.Length; i++)
            {
                Sequence bar = bars[i];
                string soundslot = sections[i]["soundslot"].StringValue;
                bar.Soundname = soundslot;
            }
        }
        public void setVolumes(SharpConfig.Section[] sections)
        {
            for (int i = 0; i < bars.Length; i++)
            {
                Sequence bar = bars[i];
                float volume = sections[i]["db"].FloatValue;
                bar.DB = volume;
            }
        }
        public void setRests(SharpConfig.Section[] sections)
        {
            for (int i = 0; i < bars.Length; i++)
            {
                Sequence bar = bars[i];
                bool rest = sections[i]["rest"].BoolValue;
                bar.Rested = rest;
            }
        }

        public Sequence[] pSequences
        {
            get { return bars; }
            set
            {
                bars = value;
            }
        }
    }
}
