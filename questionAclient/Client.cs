
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace questionA
{
    class Client
    {
        private static int port = 5001;

        public static void Main()
        {

            Request request = new Request("read", "/categories/1", "1122", "{}");

            try
            {
                TcpClient client = new TcpClient();
                Console.WriteLine("Connecting...");

                client.Connect("127.0.0.1", port);

                Console.WriteLine("Connected");
                Console.Write("Enter input: ");

                //String input = Console.ReadLine();
                String input = JsonConvert.SerializeObject(request);
                Stream stream = client.GetStream();

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] inputBytes = encoding.GetBytes(input);
                Console.WriteLine("Sending request...");

                stream.Write(inputBytes, 0, inputBytes.Length);
                byte[] streamBytes = new byte[100];
                int readStream = stream.Read(streamBytes, 0, 100);

                for (int i = 0; i < readStream; i++) Console.Write(Convert.ToChar(streamBytes[i]));
                Console.WriteLine("Client closing...");
                client.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace);
            }
        }

		public class Request
		{

			public Request(string method, string path, string date, string body)
			{
				Method = method;
				Path = path;
				Date = date;
				Body = body;
			}

			public string Method
			{
				get;
				set;
			}
			public string Path
			{
				get;
				set;
			}
			public string Date

			{
				get;
				set;
			}
			public string Body
			{
				get;
				set;
			}
		}

    }
}
