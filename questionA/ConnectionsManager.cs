using Newtonsoft.Json;
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

                    var sock = listener.AcceptTcpClient();


                    Thread thread = new Thread(() =>
                    {
                        Console.WriteLine("Connection accepted from " + sock.Client.RemoteEndPoint);                        
                        Console.WriteLine("Recieved a request...");
                        
                        var steam =sock.GetStream();
                        if (steam.DataAvailable) {
                            var request = Read(steam, sock.ReceiveBufferSize);


                            string toClient = protocol.Interpret(request);
                            ASCIIEncoding encoding = new ASCIIEncoding();
                            sock.Client.Send(encoding.GetBytes(toClient));
                            Console.WriteLine("\nResponse sent");
                        }
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

                Request Read(NetworkStream strm, int size)
                {
                    byte[] buffer = new byte[size];
                    var bytesRead = strm.Read(buffer, 0, buffer.Length);
                    var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Request: {JsonConvert.SerializeObject(request)}");
                    return JsonConvert.DeserializeObject<Request>(request);
                }
            }

        }
    }
}
