using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Server_K
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseHandler.dbinst.ConnectToDb();

            ServerC sv = new ServerC();
            sv.createServer(49001);//////////////////////////////////////////////////////
            Console.WriteLine("Server is running!");
            


        }
    }
}

