using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Server.Core.Managers;

namespace Server.Core.Communication
{
    class UdpServer
    {
        private UdpClient udpServer;
        private IPEndPoint ipEndPoint;

        private int listeningPort;

        private bool isServerRunning;

        public UdpServer(int listeningPort)
        {
            this.listeningPort = listeningPort;

            this.ipEndPoint = new IPEndPoint(IPAddress.Any, this.listeningPort);
            this.udpServer = new UdpClient(this.ipEndPoint);
        }

        public int Start()
        {
            int result = 0;
            this.isServerRunning = true;

            while (this.isServerRunning)
            {
                Console.WriteLine("Awaiting for data...");

                string[] receivedParameters = RawDataManager.GetParametersFromMessage(udpServer.Receive(ref this.ipEndPoint), "|".ToCharArray());

                Console.WriteLine("Received parameters: {0}", receivedParameters);
                Console.WriteLine("Sending to database...");

                DatabaseManager.InsertToDatabase(RawDataManager.ConvertToDictionary(DatabaseManager.Keys, receivedParameters));
            }

            return result;
        }

        public bool IsServerRunning
        {
            get { return isServerRunning; }
        }

        public UdpClient UdpServer
        {
            get { return udpServer; }
            set { udpServer = value; }
        }

        public int ListeningPort
        {
            get { return listeningPort; }
            set { listeningPort = value; }
        }
    }
}
