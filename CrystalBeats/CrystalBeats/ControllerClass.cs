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
        public Controller()
        {
            strKey = "";
            KeyboardHook.CreateHook(KeyReader);
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

        public void PlayOnce(Sequencer sequencer)
        {
            sequencer.PlaySound();
        }

        public void sendCommand()
        {

        }

        #region keyreader
        public void KeyReader(IntPtr wParam, IntPtr lParam)
        {
            int key = Marshal.ReadInt32(lParam);
            KeyboardHook.VK vk = (KeyboardHook.VK)key;
            strKey = "";

            switch (vk)
            {
                case KeyboardHook.VK.VK_F1:
                    strKey = "<-F1->";
                    break;
                case KeyboardHook.VK.VK_F2:
                    strKey = "<-F2->";
                    break;
                case KeyboardHook.VK.VK_F3:
                    strKey = "<-F3->";
                    break;
                case KeyboardHook.VK.VK_F4:
                    strKey = "<-F4->";
                    break;
                case KeyboardHook.VK.VK_F5:
                    strKey = "<-F5->";
                    break;
                case KeyboardHook.VK.VK_F6:
                    strKey = "<-F6->";
                    break;
                case KeyboardHook.VK.VK_F7:
                    strKey = "<-F7->";
                    break;
                case KeyboardHook.VK.VK_F8:
                    strKey = "<-F8->";
                    break;
                case KeyboardHook.VK.VK_F9:
                    strKey = "<-F9->";
                    break;
                case KeyboardHook.VK.VK_F10:
                    strKey = "<-F10->";
                    break;
                case KeyboardHook.VK.VK_F11:
                    strKey = "<-F11->";
                    break;
                case KeyboardHook.VK.VK_F12:
                    strKey = "<-F12->";
                    break;
                case KeyboardHook.VK.VK_NUMLOCK:
                    strKey = "<-numlock->";
                    break;
                case KeyboardHook.VK.VK_SCROLL:
                    strKey = "<-scroll>";
                    break;
                case KeyboardHook.VK.VK_LSHIFT:
                    strKey = "<-left shift->";
                    break;
                case KeyboardHook.VK.VK_RSHIFT:
                    strKey = "<-right shift->";
                    break;
                case KeyboardHook.VK.VK_LCONTROL:
                    strKey = "<-left control->";
                    break;
                case KeyboardHook.VK.VK_RCONTROL:
                    strKey = "<-right control->";
                    break;
                case KeyboardHook.VK.VK_SEPERATOR:
                    strKey = "|";
                    break;
                case KeyboardHook.VK.VK_SUBTRACT:
                    strKey = "-";
                    break;
                case KeyboardHook.VK.VK_ADD:
                    strKey = "+";
                    break;
                case KeyboardHook.VK.VK_DECIMAL:
                    strKey = ".";
                    break;
                case KeyboardHook.VK.VK_DIVIDE:
                    strKey = "/";
                    break;
                case KeyboardHook.VK.VK_NUMPAD0:
                    strKey = "0";
                    break;
                case KeyboardHook.VK.VK_NUMPAD1:
                    strKey = "1";
                    break;
                case KeyboardHook.VK.VK_NUMPAD2:
                    strKey = "2";
                    break;
                case KeyboardHook.VK.VK_NUMPAD3:
                    strKey = "3";
                    break;
                case KeyboardHook.VK.VK_NUMPAD4:
                    strKey = "4";
                    break;
                case KeyboardHook.VK.VK_NUMPAD5:
                    strKey = "5";
                    break;
                case KeyboardHook.VK.VK_NUMPAD6:
                    strKey = "6";
                    break;
                case KeyboardHook.VK.VK_NUMPAD7:
                    strKey = "7";
                    break;
                case KeyboardHook.VK.VK_NUMPAD8:
                    strKey = "8";
                    break;
                case KeyboardHook.VK.VK_NUMPAD9:
                    strKey = "9";
                    break;
                case KeyboardHook.VK.VK_Q:
                    strKey = "q";
                    break;
                case KeyboardHook.VK.VK_W:
                    strKey = "w";
                    break;
                case KeyboardHook.VK.VK_E:
                    strKey = "e";
                    break;
                case KeyboardHook.VK.VK_G:
                    strKey = "g";
                    break;
                case KeyboardHook.VK.VK_R:
                    strKey = "r";
                    break;
                case KeyboardHook.VK.VK_T:
                    strKey = "t";
                    break;
                case KeyboardHook.VK.VK_Y:
                    strKey = "y";
                    break;
                case KeyboardHook.VK.VK_U:
                    strKey = "u";
                    break;
                case KeyboardHook.VK.VK_I:
                    strKey = "i";
                    break;
                case KeyboardHook.VK.VK_O:
                    strKey = "o";
                    break;
                case KeyboardHook.VK.VK_P:
                    strKey = "p";
                    break;
                case KeyboardHook.VK.VK_A:
                    strKey = "a";
                    break;
                case KeyboardHook.VK.VK_S:
                    strKey = "s";
                    break;
                case KeyboardHook.VK.VK_D:
                    strKey = "d";
                    break;
                case KeyboardHook.VK.VK_F:
                    strKey = "f";
                    break;
                case KeyboardHook.VK.VK_H:
                    strKey = "h";
                    break;
                case KeyboardHook.VK.VK_J:
                    strKey = "j";
                    break;
                case KeyboardHook.VK.VK_K:
                    strKey = "k";
                    break;
                case KeyboardHook.VK.VK_L:
                    strKey = "l";
                    break;
                case KeyboardHook.VK.VK_Z:
                    strKey = "z";
                    break;
                case KeyboardHook.VK.VK_X:
                    strKey = "x";
                    break;
                case KeyboardHook.VK.VK_C:
                    strKey = "c";
                    break;
                case KeyboardHook.VK.VK_V:
                    strKey = "v";
                    break;
                case KeyboardHook.VK.VK_B:
                    strKey = "b";
                    break;
                case KeyboardHook.VK.VK_N:
                    strKey = "n";
                    break;
                case KeyboardHook.VK.VK_M:
                    strKey = "m";
                    break;
                case KeyboardHook.VK.VK_0:
                    strKey = "0";
                    break;
                case KeyboardHook.VK.VK_1:
                    strKey = "1";
                    break;
                case KeyboardHook.VK.VK_2:
                    strKey = "2";
                    break;
                case KeyboardHook.VK.VK_3:
                    strKey = "3";
                    break;
                case KeyboardHook.VK.VK_4:
                    strKey = "4";
                    break;
                case KeyboardHook.VK.VK_5:
                    strKey = "5";
                    break;
                case KeyboardHook.VK.VK_6:
                    strKey = "6";
                    break;
                case KeyboardHook.VK.VK_7:
                    strKey = "7";
                    break;
                case KeyboardHook.VK.VK_8:
                    strKey = "8";
                    break;
                case KeyboardHook.VK.VK_9:
                    strKey = "9";
                    break;
                case KeyboardHook.VK.VK_SNAPSHOT:
                    strKey = "<-print screen->";
                    break;
                case KeyboardHook.VK.VK_INSERT:
                    strKey = "<-insert->";
                    break;
                case KeyboardHook.VK.VK_DELETE:
                    strKey = "<-delete->";
                    break;
                case KeyboardHook.VK.VK_BACK:
                    strKey = "<-backspace->";
                    break;
                case KeyboardHook.VK.VK_TAB:
                    strKey = "<-tab->";
                    break;
                case KeyboardHook.VK.VK_RETURN:
                    strKey = "<-enter->";
                    break;
                case KeyboardHook.VK.VK_PAUSE:
                    strKey = "<-pause->";
                    break;
                case KeyboardHook.VK.VK_CAPITAL:
                    strKey = "<-caps lock->";
                    break;
                case KeyboardHook.VK.VK_ESCAPE:
                    strKey = "<-esc->";
                    break;
                case KeyboardHook.VK.VK_SPACE:
                    strKey = " "; //was <-space->
                    break;
                case KeyboardHook.VK.VK_PRIOR:
                    strKey = "<-page up->";
                    break;
                case KeyboardHook.VK.VK_NEXT:
                    strKey = "<-page down->";
                    break;
                case KeyboardHook.VK.VK_END:
                    strKey = "<-end->";
                    break;
                case KeyboardHook.VK.VK_HOME:
                    strKey = "<-home->";
                    break;
                case KeyboardHook.VK.VK_LEFT:
                    strKey = "<-arrow left->";
                    break;
                case KeyboardHook.VK.VK_UP:
                    strKey = "<-arrow up->";
                    break;
                case KeyboardHook.VK.VK_RIGHT:
                    strKey = "<-arrow right->";
                    break;
                case KeyboardHook.VK.VK_DOWN:
                    strKey = "<-arrow down->";
                    break;
                default: break;
            }
        }
        #endregion
    }
}
