using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace H1_ChatServer
{
    public class Connection
    {

        public IPAddress Ip { get; private set; }
        public int Port { get; private set; }
        public bool Connected { get; set; }
        private TcpListener tcpListener { get; set; }
        public Socket sockeForclient { get; private set; }

        public NetworkStream networkStream { get; set; }
        public StreamReader streamReader { get; set; }
        public StreamWriter streamWriter { get; set; }



        public Connection(IPAddress ip, int port)
        {
            Connected = true;
            this.Ip = ip;
            this.Port = port;
        }

        public void ListenForConnections()
        {
            tcpListener = new TcpListener(Ip, Port);
            tcpListener.Start();
        }

        public void ConnectToClient()
        {
            try
            {
                sockeForclient = tcpListener.AcceptSocket();
            }
            catch
            {
                Console.WriteLine("Connection error");
            }
        }

        public void ClientConnection()
        {
            networkStream = new NetworkStream(sockeForclient);
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
        }


        public void disconect()
        {
            streamWriter.Flush();
            networkStream.Close();
            streamReader.Close();
            streamWriter.Close();
            sockeForclient.Close();
        }
    }
}