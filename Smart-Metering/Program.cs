using Smart_Metering.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Smart_Metering
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to Schneider Electric product");
            DoSmartMetering();
        }

        protected  static void DoSmartMetering()
        {
            try
            {
                int data = 0;
                while (data != 1 && data != 2)
                {
                    Console.WriteLine("Do you want export data in file(1) or SQL(2)");

                    data = Convert.ToInt32(Console.ReadLine());
                }
                switch (data)
                {
                    case 2:     //Connection to BBDD
                        Console.WriteLine("Connecting to SQL");      //Ask about the credentials to connect to the BBDD
                        ConnectionClient Connection = new ConnectionClient();
                        SqlConnection connect = Connection.InitConnection();
                        AddElementSQL(connect);
                        break;
                    case 1:     //Save in file
                        ConfigurationFile file = new ConfigurationFile();
                        AddElementFile(file.ConfigureFile());
                        break;
                    default:
                        Console.WriteLine("Incorrect value");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                DoSmartMetering();      //return execute program in case of error
            }
        }

        #region AddElements
        protected static void AddElementSQL(SqlConnection connect)
        {
            //Responsible for adding parameters in database
            Dictionary<string, string> elements = new Dictionary<string, string>();

            //Read elements
            Console.Clear();
            ReadParameters obj = new ReadParameters();
            elements = obj.DoRead();

            //Store elements to BBDD
            StoreSQL store = new StoreSQL();
            store.WriteSQL(elements, connect);

            //Close application or add a new element
            Console.WriteLine("Do you want to add a new item? (yes/not)");
            if (Console.ReadLine().ToLower().Equals("yes"))
                AddElementSQL(connect);

            //End program
            Environment.Exit(0);
        }

        protected static void AddElementFile(string path)
        {
            //Responsible for adding parameters in file
            Dictionary<string, string> elements = new Dictionary<string, string>();

            //Read elements
            Console.Clear();
            ReadParameters obj = new ReadParameters();
            elements = obj.DoRead();

            //Store elements to File
            ConfigurationFile store = new ConfigurationFile();
            store.StoreFile(elements, path);

            //Close application or add a new element
            Console.WriteLine("Do you want to add a new item? (yes/not)");
            if (Console.ReadLine().ToLower().Equals("yes"))
                AddElementFile(path);

            //End program
            Environment.Exit(0);
        }
        #endregion
    }
}
