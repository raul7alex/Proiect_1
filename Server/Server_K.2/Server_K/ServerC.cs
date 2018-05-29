using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server_K
{
    class ServerC : ServerS_K // mostenim clasa Server S;
    {
        
        private Thread th = null;
        bool run = true; // daca serverul trebuie sa ruleze;

        public void createServer(int port)
        {
            creareSocket(port);
            th = new Thread(new ThreadStart(start));
            th.Start();
        }

        private void start()
        {
           
            while(run  == true)
            {
                
                Socket sk = returnSocket();
                Console.WriteLine("Conectare acceptata de la "+ sk.RemoteEndPoint);
               

               
                ClientHandler_K cl = new ClientHandler_K(sk); // creare client 
                cl.InitializareC();
                Thread.Yield();  // elibereaza procesorul pentru alt thread;
                Thread.Sleep(1);
            }
            
        }

        public void oprireServer()
        {
            oprireSocket();
        }
    }
}
