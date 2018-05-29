using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SQLite;

namespace Server_K
{

    class ServerS_K
    {
        private Socket _sk = null;

        protected void creareSocket(int port) // metoda de creare a unui socket pe un port.
        {
            if (null != _sk)  // daca e null => socketul exista;
            {
                throw new Exception("Socketul exista"); 
            }

            _sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //cream socketul;
            try
            {
                _sk.Bind(new IPEndPoint(IPAddress.Any, port)); // il legam la port;
                _sk.Listen(10);
                Console.WriteLine(IPAddress.Any);////////////////////////////////////////////////////////////////////////////
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected Socket returnSocket() // metoda cu care trimitem conexiunea cu clientu la core
        {
            try
            {
                return _sk.Accept();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void oprireSocket() // metoda de oprire a socketului;
        {
            if (null == _sk)
                return;
            _sk.Close();
            _sk = null;
        }

    }
}
