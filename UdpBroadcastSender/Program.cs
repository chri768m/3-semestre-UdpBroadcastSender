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
        private static readonly Random Getrandom = new Random();

        public static int GetRandomNumber()
        {
            
            lock (Getrandom) // synchronize
            {
                CN = Getrandom.Next(CN - 1, CN + 2);
                return CN;
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
                string message = "" + CN;
                byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
                socket.Send(sendBuffer, sendBuffer.Length, endPoint);
                Console.WriteLine("Message sent to broadcast address {0} port {1}", endPoint.Address, Port);
                Thread.Sleep(5000);
                GetRandomNumber();
            }
        }
    }
}
