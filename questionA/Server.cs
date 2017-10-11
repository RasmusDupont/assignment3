using System;
namespace questionAserver
{
    class Server
    {


        static void Main(string[] args)
        {
            new ConnectionsManager(5001);
        }
    }
}