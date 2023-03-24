using System.Net;

namespace H1_ChatServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            Int32 port = 8000;
            Connection server = new Connection(ipAddress, port);

            server.ListenForConnections();
            server.ConnectToClient();

            Console.WriteLine("Connection established");


            string messageSent = "";
            string messageRecieved = "";


            try
            {
                server.ClientConnection();

                while (server.Connected)
                {

                    if (server.sockeForclient.Connected)
                    {
                        messageRecieved = server.streamReader.ReadLine();
                        Console.WriteLine(messageRecieved);

                        if (messageRecieved == "exit")
                        {
                            server.Connected = false;
                            server.streamReader.Close();
                            server.streamWriter.Close();
                            server.networkStream.Close();
                            return;
                        }

                        Console.Write("Me : ");
                        messageSent = Console.ReadLine();
                        server.streamWriter.WriteLine(messageSent);
                        server.streamWriter.Flush();
                    }
                }
                server.disconect();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }
    }
}