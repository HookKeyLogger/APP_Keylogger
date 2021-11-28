using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace key
{
    public class ProcessActivity
    {
            private static List<appActivity> start = new List<appActivity>();

        static void WriteLog(string process)
        {
            API.CreateFolderText();
            API.CreateFolderDay();
            string logNameToWrite = API.FolderDay + "/" + API.ProcessActivity + API.logExtendtion;
            StreamWriter sw = new StreamWriter(logNameToWrite, true);
            sw.Write(process);
            sw.Close();
        }
        private static Process GetProcessByHandle(IntPtr hwnd)
            {
                try
                {
                    uint processID;
                    API.GetWindowThreadProcessId(hwnd, out processID);
                    return Process.GetProcessById((int)processID);
                }
                catch { return null; }
            }

            private static Process GetActiveProcess()
            {
                IntPtr hwnd = API.GetForegroundWindow();
                return hwnd != null ? GetProcessByHandle(hwnd) : null;
            }

            private static bool ListAndCheck(appActivity a)
            {
                bool check = false;
                start.Add(a);
                if (start.Count == 1) check = true;
                else
                {
                    var duplicateExists = start.Where(x => x.NameProcess == a.NameProcess && x.Title == a.Title && x.Time == a.Time && x.Path == a.Path).Count();
                    if (duplicateExists > 1)
                    {
                        check = false;
                    }
                    else
                    {
                        check = true;
                    }
                }
                return check;
            }


            public static void NotificationWhenActivate()
            {
                List<string> arrayprocess = new List<string>();
                while (true)
                {
                    Process p = GetActiveProcess();
                    if (p != null)
                    {
                        try
                        {
                            appActivity a = new appActivity();

                            a.NameProcess = p.ProcessName.ToString();
                            a.Title = p.MainWindowTitle.ToString();
                            a.Time = p.StartTime.ToString("dd-MM-yyyy hh::mm::ss tt");
                            a.Path = p.MainModule.FileName.ToString();

                            string process = "NameProcess: " + a.NameProcess + Environment.NewLine +
                                             "MainWindownTitle: " + a.Title + Environment.NewLine +
                                             "TimeExcute: " + a.Time + Environment.NewLine +
                                             "Path: " + a.Path;

                            if (ListAndCheck(a) == true)
                            {
                                Console.Write("\n\n" + process + "\n\n");
                                arrayprocess.Add(process);
                                WriteLog("\n\n" + process + "\n\n");
                            }
                            if (arrayprocess[arrayprocess.Count - 1] != process)
                            {
                                Console.Write("\n\n" + process + "\n\n");
                                arrayprocess.Add(process);
                                WriteLog("\n\n" + process + "\n\n");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine();
                            //Console.WriteLine(e.ToString());
                        }
                    }
                Thread.Sleep(1000);
                }
        }
    }
}
