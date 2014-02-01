using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Core.Communication;

namespace Server
{
    class Program
    {
        private static UdpServer udpServer;

        static void Main(string[] args)
        {
            Console.WriteLine("Initializing server...");
            udpServer = new UdpServer(6100);

            int result = udpServer.Start();

            Console.WriteLine("Server finished with result code: {0}", result);
        }
    }
}
