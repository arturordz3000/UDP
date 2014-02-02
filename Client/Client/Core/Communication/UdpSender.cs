using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client.Core.Communication
{
    class UdpSender
    {
        private UdpClient updClient;
        private IPAddress endpointIPAddress;
        private string ipAddress;
        private int port;

        public UdpSender(string ipAddress, int port)
        {
            this.endpointIPAddress = IPAddress.Parse(ipAddress);
            this.updClient = new UdpClient();
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public void Start()
        {
            Console.WriteLine("Connecting to ip: {0}", this.ipAddress);
            this.updClient.Connect(this.endpointIPAddress, this.port);
        }

        public void SendMessage(string message)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            this.updClient.Send(messageBytes, messageBytes.Length);
        }

        public void Close()
        {
            this.updClient.Close();
        }
    }
}
