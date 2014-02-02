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
            udpSender = new UdpSender("127.0.0.1", 6100);

            udpSender.Start();
            udpSender.SendMessage("2014-01-27 17:26:08|1940.86426,09911.36117,5.500,0,3.0|0,187.90;0,226.81;");
            udpSender.Close();
        }
    }
}
