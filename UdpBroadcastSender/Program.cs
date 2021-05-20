using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UdpBroadcastSender
{
    class Program
    {
        public static int CN = 20;
        public static int HN = 70;
        public static int SN = 75;
        public static int CON = 700;
        private static int _nextId = 1;
        private static readonly Random Getrandom = new Random();

        public static int GetRandomTemp()
        {
            
            lock (Getrandom) // synchronize
            {
                CN = Getrandom.Next(CN - 1, CN + 2);
                return CN;
            }
        }

        public static int GetRandomStoej()
        {
            lock (Getrandom) // synchronize
            {
                SN = Getrandom.Next(SN - 1, SN + 2);
                return SN;
            }
        }

        public static int GetRandomHumi()
        {
            lock (Getrandom) // synchronize
            {
                HN = Getrandom.Next(HN - 1, HN + 2);
                return HN;
            }
        }
        public static int GetRandomCO2()
        {
            lock (Getrandom) // synchronize
            {
                CON = Getrandom.Next(CON - 1, CON + 2);
                return CON;
            }
        }


        public const int Port = 8400;
        static void Main()
        {
            UdpClient socket = new UdpClient();
            socket.EnableBroadcast = true; // IMPORTANT
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, Port);
            while (true)
            {
                GetRandomHumi();
                GetRandomTemp();
                GetRandomCO2();
                GetRandomStoej();
                Measurement Meas = new Measurement(CN, HN, SN, CON, _nextId++);
                string message = "" + Meas;
                byte[] sendBuffer = Encoding.ASCII.GetBytes(Meas.ToString());
                socket.Send(sendBuffer, sendBuffer.Length, endPoint);
                Console.WriteLine("Message sent to broadcast address {0} port {1}", endPoint.Address, Port);
                Thread.Sleep(5000);
            }
        }
    }
}
    