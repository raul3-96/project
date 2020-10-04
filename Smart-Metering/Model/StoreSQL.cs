using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Smart_Metering.Model
{
    public class StoreSQL
    {
        public void WriteSQL(Dictionary<string, string> obj, SqlConnection connection)
        {
            //Prepare query extraiting information from array
            string id ="",elementtype="", serialnumber = "", brand = "", model = "", ip = "", port = "", query ="";
            List<string> Listdata = new List<string>();

            foreach (KeyValuePair<string, string> element in obj)
            {
                Listdata.Add(element.Value);
            }

            //Extract information
            for(int i = 0; i < Listdata.Count; i++)
            {
                switch(i)
                {
                    case 0:
                        elementtype = Listdata[i];
                        break;
                    case 1:
                        id = Listdata[i];
                        break;
                    case 2:
                        serialnumber = Listdata[i];
                        break;
                    case 3:
                        brand = Listdata[i];
                        break;
                    case 4:
                        model = Listdata[i];
                        break;
                    case 5:
                        ip = Listdata[i];
                        break;
                    case 6:
                        port = Listdata[i];
                        break;
                }
            }

            if (elementtype.Equals(""))
                Console.WriteLine("Empty Element");
            else  if(elementtype.Equals("1") || elementtype.Equals("2"))
                query = "insert into dbo.element_meter values ("+ id+","+ elementtype + ", '" + serialnumber + "', '" + brand + "', '" + model + "');";
            else if(elementtype.Equals("3"))
                query = "insert into dbo.Gateway values (" +id + ","+ elementtype + ", '" + serialnumber + "', '" + brand + "', '" + model + "','" + ip + "', '" + port + "');";

            SendQuery(query, connection);
        }

        protected void SendQuery(string query, SqlConnection connection)
        {
            //Run the query
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Element added");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
