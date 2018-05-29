using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Server_K
{
    class DatabaseHandler
    {
        private SQLiteConnection _conn;
        private static DatabaseHandler _handl = new DatabaseHandler();
        private DatabaseHandler() { }
        public static DatabaseHandler dbinst
        {
            get
            {
                return _handl;
            }
        }
        public void ConnectToDb()
        {
            if (!File.Exists("MyDatabase.sqlite"))
            { SQLiteConnection.CreateFile("MyDatabase.sqlite"); }
            try
            {
                _conn = new SQLiteConnection("Data Source=MyDatabase.sqlite; Version=3;");
                _conn.Open();
                Console.WriteLine("Conectare cu succes la baza de date!");
                createTable();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Conectare nereusita!/n ERROR:" + ex.ToString());
                return;
            }

        }
        public void DisconnectFromDb()
        {
            if (null != _conn)
            {
                _conn.Close();
                _conn.Dispose();
                _conn = null;
                Console.WriteLine("A-ti fost deconectat!");
            }

        }

        public void createTable()
        {
            string stmt = "CREATE TABLE IF NOT EXISTS Users(ID INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT, Password TEXT, Nume TEXT)";
            SQLiteCommand cmd = new SQLiteCommand(stmt, _conn);
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:" + ex.ToString());
            }
        }

        public void insert(string Username, string Password, string Nume)
        {
            string pass = Hash.Crypt(Password);


            string stmt = "INSERT INTO Users(Username,Password,Nume) VALUES('" + Username + "','" + pass + "', '" + Nume + "' )";
            SQLiteCommand cmd = new SQLiteCommand(stmt, _conn);
            Console.WriteLine("Datele au fost adaugate!");
            try
            {
                cmd.ExecuteNonQuery();
                createTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:" + ex.ToString());
            }
        }

        public bool CheckUser(string Username, string Password, string Nume)
        {
            lock (_handl)
            {
                string pass = Hash.Crypt(Password);
                
                SQLiteCommand command = new SQLiteCommand(String.Format("select * from  Users where Username='{0}' and Password='{1}' and Nume='{2}' ", Username, pass,Nume), _conn);
                try
                {
                    if (command.ExecuteScalar() != null)
                    {

                        return true;
                    }
                    else

                        return false;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return false;
            }
        }


        public bool GetUser(string Username)
        {
            lock (_handl)
            {
                SQLiteCommand command = new SQLiteCommand(String.Format("select * from Users  where Username='{0}'", Username), _conn);

                if (command.ExecuteScalar() != null)
                {

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }

    }

}


    