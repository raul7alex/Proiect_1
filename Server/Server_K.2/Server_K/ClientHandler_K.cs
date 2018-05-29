using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace Server_K
{
    

    class ClientHandler_K
    {
        
        String mesaj;
        string id;
        private Socket sk = null;
        private Thread th = null;
        private bool run = true;
        ASCIIEncoding asen = new ASCIIEncoding();
        

        byte[] rawMsg = new byte[1024];
        char[] split1 = { '-' };
        char[] split2 = { '.' };

        string[] user = null;
        string[] pass = null;
        string[] nume = null;
     

        public ClientHandler_K(Socket _sk)
        {
            sk = _sk; // referinta de la soket 
            
        }

        public void InitializareC()
        {
            
            th = new Thread(new ThreadStart(start));
            th.Start();
            
        }





        public void start() // aici asteapta sa fie trimis ceva catre sv si afiseaza pe consola
        {

            

            Login();
            Return ob = new Return(nume[0]);
            sk.Send(asen.GetBytes("\n\n Logare reusita, utilizator : " + ob.GetNume()));
            


            sk.Send(asen.GetBytes("\n  1-Trimite mesaj la server , 2 - Logout \n"));
            byte[] rawMsg = new byte[1];
            
            while (run == true)
            {
                try
                {
                    int bCount = sk.Receive(rawMsg);
                    
                    if (bCount <= 0)
                    {
                        return;
                    }                  
                    mesaj = Encoding.UTF8.GetString(rawMsg);
                    
                    switch (mesaj)
                    {
                       
                        case "1": Chat(ob.GetNume()); break;
                        case "2": Logout(); break;
                        default: sk.Send(asen.GetBytes("\n optiunea aleasa nu este corecta \n")); break;
                                      
                    }

                    Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    return;
                }
                Thread.Sleep(1);
            }
        }

        private void Login() { 
        
            bool var = true;
            do
            {
                sk.Send(asen.GetBytes("\n Nu poti folosi nici o comanda pana cand nu te logezi!!! cu exceptia inregistrarii \n"));
                sk.Send(asen.GetBytes("\n Comenzile sunt 1 - Logare , 2 - Deconectare 3 - Inregistrare "));

                mesaj = GetCommand();

                if (mesaj == "2")
                {
                    Logout();
                    
                }
                if (mesaj == "3")
                {
                    Register();
                }
   
                if (mesaj == "1")
                {


                    sk.Send(asen.GetBytes("\n Login , username-nume-password. :  \n"));

                    int bCount = sk.Receive(rawMsg);
                    if (bCount <= 0)
                    {
                        return;
                    }
                    mesaj = Encoding.UTF8.GetString(rawMsg);
                    user = mesaj.Split(split1);
                    nume = user[1].Split(split1);
                    pass = user[2].Split(split2);


                    if (DatabaseHandler.dbinst.CheckUser(user[0], pass[0], nume[0]))
                    {                                     
                        var = false;
                    }
                    else
                    {
                        sk.Send(asen.GetBytes("\n Username sau parola gresita \n"));
                        
                    }
                }
            } while (var);
        }

        private void Register()
        {
            sk.Send(asen.GetBytes("\n Introdu Username , password si numele sub forma   Username-nume-password. \n"));

            int bCount = sk.Receive(rawMsg);
            if (bCount <= 0)
            {
                return;
            }
            mesaj = Encoding.UTF8.GetString(rawMsg);
            user = mesaj.Split(split1);
            nume = user[1].Split(split1);
            pass = user[2].Split(split2);

            if (DatabaseHandler.dbinst.GetUser(user[0]))
            {
                sk.Send(asen.GetBytes("\n Alege alt username; \n"));
                return;
            }
            
            DatabaseHandler.dbinst.insert(user[0], pass[0],nume[0]);
            sk.Send(asen.GetBytes("\n Te-ai inregistrat \n"));
        }
       

        private void Logout()
        {
            sk.Send(asen.GetBytes("\n Disconected \n "));
            sk.Close();
        }

        private void Chat(string id)
        {
            
            sk.Send(asen.GetBytes("\n trimite mesaj la server  \n"));
            Console.WriteLine("User " + id + ": " + GetString() );
            sk.Send(asen.GetBytes("\n iesire din chat  \n"));
        }



        private string GetCommand()
        {
            byte[] rawMsg1 = new byte[1];
            int bCount1 = sk.Receive(rawMsg1);
            if (bCount1 <= 0)
            {
                return null;
            }
            
            return Encoding.UTF8.GetString(rawMsg1);
        }

       

        private string GetString()
        {
            byte[] rawMsg = new byte[100];
            int c = sk.Receive(rawMsg);
            if (c <= 0)
            {
                return null;
            }

            return Encoding.UTF8.GetString(rawMsg);
        }

    }
}
