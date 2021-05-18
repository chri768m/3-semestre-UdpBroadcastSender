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

        public static int GetRandomHumi()
        {
            lock (Getrandom) // synchronize
            {
                HN = Getrandom.Next(HN - 1, HN + 2);
                return HN;
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
                Measurement Meas = new Measurement(CN, HN, _nextId++);
                string message = "" + Meas;
                byte[] sendBuffer = Encoding.ASCII.GetBytes(Meas.ToString());
                socket.Send(sendBuffer, sendBuffer.Length, endPoint);
                Console.WriteLine("Message sent to broadcast address {0} port {1}", endPoint.Address, Port);
                Thread.Sleep(5000);
            }
        }
    }
}
    