using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


namespace CFG_mng
{ 
    public class INI_mng
    { 
        struct cfg_obj
        {
            public string cfg_name;
            public string cfg_value;
            public string cfg_sector;

            public cfg_obj(string gl_name, string name, string value)
            {
                cfg_sector = gl_name;
                cfg_name = name;
                cfg_value = value;
            }

            public void GetInfo()
            {
                Console.WriteLine($"Раздел: {cfg_sector} параметр: {cfg_name} значение параметра: {cfg_value}");
            }

            public void GetValue()
            {
                Console.WriteLine($"Значение параметра: {cfg_value}");
            }


        };
    char open_cat = '[', close_cat = ']', value_indecator = '=';
        private string Pathfile { get; }

        UInt16 numstring
        {
            get { return numstring; }

            set { numstring = value; }
        }

         string value_of_parametr
        {
            get { return value_of_parametr; }

            set { value_of_parametr = value; }
        }

     string name_of_parametr
        {
            get { return name_of_parametr; }

            set { name_of_parametr = value; }
        }

     string name_of_sector
     {
        get
         {
             return name_of_sector;
         }
        set
         {
             name_of_sector = value;
         }
     }

        string catlog = @"C:\Users\Андрейка\source\repos\OOP_LABS_AD\Lab_1\config_test.ini";
        public INI_mng(string path)
        {
            Pathfile = path;
            numstring = 0;
            ReadINI();
        }

        void NewRec()
        {
            var RecP = new cfg_obj
            {
                cfg_value = value_of_parametr,
                cfg_name = name_of_parametr,
                cfg_sector = name_of_sector
            };

        }
        public void ReadINI()

        {
            var fileInfo = new FileInfo(Pathfile);
            if (fileInfo.Exists && fileInfo.Extension == ".ini")
            {
                try
                {
                    using (StreamReader sr = new StreamReader(catlog, System.Text.Encoding.Default))
                    {
                        string textLine;
                        while ((textLine = sr.ReadLine()) != null))
                        {
                            numstring++;
                            if(cfg_obj(textLine))

                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message + " in " + Pathfile);
                    throw;
                }
            }
            else
            {
                throw new FileNotFoundException("Место расположения недоступно " + Pathfile);
            }
        }
        public bool objectString(string textline)
        {


        }
    }
}

