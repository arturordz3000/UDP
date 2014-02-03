using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Core.Communication;

namespace Client
{
    class Program
    {
        private static UdpSender udpSender;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting server...");
            udpSender = new UdpSender("148.244.156.42", 6100);

            udpSender.Start();

            String now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            udpSender.SendMessage(now + "|1940.86426,09911.36117,5.500,0,3.0|0,187.90;0,226.81;");
            udpSender.Close();
        }
    }
}
