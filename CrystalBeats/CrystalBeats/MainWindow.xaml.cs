using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharpDX.DirectInput;
using System.Threading;
using System.Runtime.InteropServices;

namespace CrystalBeats
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Joystick joystick;
        Controller xControl;
        string strKey;
        string strField;

        Timer tHopScotch;

        public MainWindow()
        {
            InitializeComponent();
            KeyboardHook.CreateHook(KeyReader);
            this.tHopScotch = new Timer();
            this.tHopScotch.Mode = TimerMode.Periodic;
            this.tHopScotch.Tick += new EventHandler(this.tHopScotch_Tick);
            xControl = new Controller();
            // StartReadingThread();
           
            this.DataContext = new ViewModel();
            tHopScotch.Period = 5;
            tHopScotch.Resolution = 999999999;
            InitiallizeGamePad();
        }

        private void tHopScotch_Tick(object sender, EventArgs e)
        {
            var datas = joystick.GetBufferedData();
            foreach (var state in datas)
            {
                strField = state.Offset.ToString();

                //Action<string> action = new Action<string>(SendCommand);
                if (state.Value == 128) xControl.sendCommand(strField);

            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            bool bPressed = false;
            if (!bPressed)
            {
                ((ViewModel)(this.DataContext)).cController.sendCommand(strKey);
                bPressed = true;
            }
        }


        private void InitiallizeGamePad()
        {
            // Initialize DirectInput
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                        DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                        DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid == Guid.Empty)
            {

                MessageBox.Show("HopScotch Matte nicht gefunden! ");

            }
            else
            {
                // Instantiate the joystick
                joystick = new Joystick(directInput, joystickGuid);

                Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

                // Query all suported ForceFeedback effects
                var allEffects = joystick.GetEffects();
                foreach (var effectInfo in allEffects)
                    Console.WriteLine("Effect available {0}", effectInfo.Name);

                // Set BufferSize in order to use buffered data.
                joystick.Properties.BufferSize = 128;

                // Acquire the joystick
                joystick.Acquire();

                tHopScotch.Start();
            }

        }
        public void HopScotchReader()
        {
            while (true)
            {
                var datas = joystick.GetBufferedData();
                foreach (var state in datas)
                {
                    strField = state.Offset.ToString();
                    if (state.Value == 128) ((ViewModel)(this.DataContext)).cController.sendCommand(strField);
                }

            }
        }

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

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            xControl = ((ViewModel)(this.DataContext)).cController;
        }
    }
}

