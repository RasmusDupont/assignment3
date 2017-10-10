using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace questionA
{
    class Server
    {
        private static int port = 5000;

        static void Main(string[] args)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();

                Console.WriteLine("Running on port " + 5000);
                Console.WriteLine("Listener endpoint: " + listener.LocalEndpoint);
                Console.WriteLine("Waiting for a connection...");

                Socket sock = listener.AcceptSocket();
                Console.WriteLine("Connection accepted from " + sock.RemoteEndPoint);

                byte[] bytes = new byte[100];
                int numOfBytes = sock.Receive(bytes);
                Console.WriteLine("Recieved a request...");
                for (int i = 0; i < numOfBytes; i++) Console.Write(Convert.ToChar(bytes[i]));

                ASCIIEncoding encoding = new ASCIIEncoding();
                sock.Send(encoding.GetBytes("Request received"));
                Console.WriteLine("\nResponse sent");

                Console.WriteLine("Socket closing...");
                sock.Close();
                //listener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace);
            }
        }
    }
}