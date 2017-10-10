
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace questionA
{
    class Client
    {
        private static int port = 5000;

        public static void Main()
        {

            try
            {
                TcpClient client = new TcpClient();
                Console.WriteLine("Connecting...");

                client.Connect("127.0.0.1", port);

                Console.WriteLine("Connected");
                Console.Write("Enter input: ");

                String input = Console.ReadLine();
                Stream stream = client.GetStream();

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] inputBytes = encoding.GetBytes(input);
                Console.WriteLine("Sending request...");

                stream.Write(inputBytes, 0, inputBytes.Length);
                byte[] streamBytes = new byte[100];
                int readStream = stream.Read(streamBytes, 0, 100);

                for (int i = 0; i < readStream; i++) Console.Write(Convert.ToChar(streamBytes[i]));

                client.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace);
            }
        }
    }
}
