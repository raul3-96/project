using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_Metering.Model
{
    class ReadParameters
    {
        #region vars
        Dictionary<string, string> Meter = new Dictionary<string, string>();
        int attempts = 0;
        #endregion


        public Dictionary<string, string> DoRead()
        {
            //Select the element type
            Console.WriteLine("What element type need to configure?(1-Electrical Meter / 2-Water Meter / 3-Gateway)");  
            string metertype = Console.ReadLine();   

            #region Meters/Gateway
            if (Convert.ToInt32(metertype) <=3)         //Value in range
            {
                Meter.Add("id_metertype",metertype);

                //ID
                RequestData("id", "Introduce the meter id:");   
                while (CheckData())                           
                {
                    Meter.Remove("id");                         
                    RequestData("id", "Introduce the meter id:");
                }
                
                //Serial number
                RequestData("serialnumber", "Introduce the serial number:");
                while (CheckData())
                {
                    Meter.Remove("serialnumber");
                    RequestData("serialnumber", "Introduce  the serial number:");
                }

                //Brand
                RequestData("brand", "Introduce the meter brand:");

                //Model
                RequestData("model", "Introduce the meter model:");

                if (metertype.Equals("3"))
                {
                    //IP
                    RequestData("ip", "Introduce the IP:");
                    while (CheckData())
                    {
                        Meter.Remove("ip");
                        RequestData("ip", "Introduce the IP:");
                    }

                    //Port
                    RequestData("port", "Introduce the port:");
                }
            }
            else                                     
            {
                Console.WriteLine("Incorrect Data");
                DoRead();
            }
            #endregion

            return Meter;
        }

        #region References
        protected void RequestData(string column, string message)
        {
            //Request the parameters y checked how many attemps it carried
            if (attempts >= 3)
            {
                Console.WriteLine("It takes too many tries");
                attempts = 0;
                Meter.Clear();
                DoRead();
            }
            else { 
                Console.WriteLine(message);
                Meter.Add(column, Console.ReadLine());
            }
        }

        protected bool CheckData()
        {
            //Check if the information recived is correct, which is obligatory. Also if the data is numeric
            int position = 1;
            foreach (KeyValuePair<string, string> data in Meter)
            {
                if (position == Meter.Count)
                {
                    if (string.IsNullOrEmpty(data.Value))
                    {
                        Console.WriteLine("input string was null");
                        attempts++;
                        return true;
                    }
                    else if(data.Key.Equals("id"))      //Numerical values
                    {
                        try
                        {
                            Convert.ToInt32(data.Value);
                            attempts = 0;
                            return false;
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                            attempts ++;
                            return true;
                        }
                    } 
                    else
                    {
                        attempts = 0;
                        return false;
                    }
                }
                position++;
            }
            return false;
        }
        #endregion
    }
}
