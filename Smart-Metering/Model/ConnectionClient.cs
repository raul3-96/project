using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Smart_Metering.Model
{
    public class ConnectionClient
    {
        public SqlConnection InitConnection()
        {
            //Configure connection to the database
            string servername, login, password;

            Console.Write("Server name:");
            servername = Console.ReadLine();

            Console.Write("Login:");
            login = Console.ReadLine();

            Console.Write("Password:");
            password = Console.ReadLine();

            //Configure connection command
            SqlConnection connection = new SqlConnection("server=" + servername + ";database=ProductSmartMeter;Uid=" + login + ";pwd=" + password);

            //Connection is checked
            if(!TryConnect(connection))
            {
                connection.Close();
                InitConnection();
            }

            return connection;
        }

        protected bool TryConnect(SqlConnection conexion)
        {
            try
            {
                Console.WriteLine("Connecting...");
                conexion.Open();
                Console.WriteLine("Connection successfully");
                return true;
            }
            catch
            {
                Console.WriteLine("Connection refused");
                return false;
            }
        }
    }
}
