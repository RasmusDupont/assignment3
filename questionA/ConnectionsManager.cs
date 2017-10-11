using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace questionAserver
{

    public class ConnectionsManager
    {
        TcpListener listener;

        RDJTP protocol; 

        public ConnectionsManager(int port)
        {
            protocol = new RDJTP();

            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();

            while (true)
            {
                try
                {
                    Console.WriteLine("Running on port " + port);
                    Console.WriteLine("Listener endpoint: " + listener.LocalEndpoint);
                    Console.WriteLine("Waiting for a connection...");

                    Socket sock = listener.AcceptSocket();


                    Thread thread = new Thread(() =>
                    {
                        Console.WriteLine("Connection accepted from " + sock.RemoteEndPoint);

                        byte[] bytes = new byte[100];
                        int numOfBytes = sock.Receive(bytes);
                        Console.WriteLine("Recieved a request...");

                        string fromClient = "";
                        for (int i = 0; i < numOfBytes; i++)
                            fromClient  += Convert.ToChar(bytes[i]);
                        Console.Write(fromClient);

                        string toClient = protocol.Interpret(fromClient);

                        ASCIIEncoding encoding = new ASCIIEncoding();
                        sock.Send(encoding.GetBytes(toClient));
                        Console.WriteLine("\nResponse sent");

                        Console.WriteLine("Socket closing...");
                        sock.Close();
                    });
                    thread.Start();

                    //listener.Stop();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.StackTrace);
                }
            }

        }
    }
}
