using System;
using System.Collections.Generic;

namespace questionAserver
{
    class Server
    {


        static void Main(string[] args)
        {       
            
            CategoryList.Getlist().Add(new Category(1, "Beverages"));
            CategoryList.Getlist().Add(new Category(2, "Condiments"));
            CategoryList.Getlist().Add(new Category(3, "Confections"));
            new ConnectionsManager(5001);
        }
    }
}