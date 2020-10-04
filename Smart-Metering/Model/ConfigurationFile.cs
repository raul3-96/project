using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Smart_Metering.Model
{
    class ConfigurationFile
    {
        #region Configuration
        public string ConfigureFile()
        {
            //Configure where file is located
            Console.Write("Introduce the file path:");

            string path = Console.ReadLine();

            while (!File.Exists(path))
            {
                Console.Write("Incorrect path. Enter the file path again:");
                path = Console.ReadLine();
            }
            return path;
        }
        #endregion

        #region Store
        public void StoreFile(Dictionary<string, string> elements, string path)
        {
            //Store parameters in file selected
            try
            {
                foreach (KeyValuePair<string, string> element in elements)
                {
                    using (StreamWriter outputFile = new StreamWriter(path, true))
                    {
                        outputFile.WriteLine(element.Key + ":" + element.Value);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion
    }
}
