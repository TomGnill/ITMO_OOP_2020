using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


namespace CFG_mng
{
    public class interactive_parser
    {
        Dictionary<param_obj, string> param_list = new Dictionary<param_obj, string>();
        string catlog = @"C:\Users\Андрейка\source\repos\OOP_LABS_AD\Lab_1\config_test.ini";
        private string Pathfile { get; }

        public UInt16 numstring
        {
            get { return numstring; }
            set { numstring = value; }

        }

        public string value_of_parametr
        {
            get { return value_of_parametr; }
            set { value_of_parametr = value; }
        }

        public string name_of_parametr
        {
            get { return name_of_parametr; }
            set { name_of_parametr = value; }
        }

        public string name_of_sector
        {
            get { return name_of_sector; }
            set { name_of_sector = value; }
        }

        enum func_num
        {
            open_file = 1,
            par_list,
            int_search,
            exit,
        }

        static void inter_menu(func_num number_of_func)
        {

            switch (number_of_func)
            {
                case func_num.open_file:
                    ini_open();
                    break;
                case func_num.par_list:
                    printList();
                    ;
                    break;
                case func_num.int_search:
                    searchParam();
                    ;
                    break;
                case func_num.exit:
                    exit_interactive();
                    ;
                    break;
                default:
                    Console.WriteLine(" Вы выбрали несуществующее действие!");
                    break;

            }
        }



        public struct param_obj
        {
            public string param_name;
            public string param_sector;
        }

        public ini_open(string path)
        {
            Pathfile = path;
            numstring = 0;
            ini_reader();
        }

        void new_param()
        {
            var record_param = new param_obj
            {
                param_name = name_of_parametr,
                param_sector = name_of_sector
            };
            param_list.Add(record_param, name_of_sector);
        }

        public void searchParam()
        {
            foreach (var (key, value) in param_list)
            {
                Console.WriteLine(key.param_name + " - " + key.param_sector + " - " + value);
            }

        }

        public int string_reader(string file_string)
        {
            if (file_string.Trim().Length == 0)
                return 0;
            if (file_string.StartsWith("[") && file_string.EndsWith("]"))
            {

            }
            if 


        }
        public void ini_reader()
        {

            var fileInfo = new FileInfo(Pathfile);
            if (fileInfo.Exists && fileInfo.Extension == ".ini")
            {
                try
                {
                    using (StreamReader sr = new StreamReader(catlog, System.Text.Encoding.Default))
                    {
                        string textLine;
                        while ((textLine = sr.ReadLine()) != null)
                        {
                            numstring++;
                            if (string_reader(file_string) == 1)
                            {
                                new_param();
                            }

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
    }

    /*struct cfg_obj
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

        private string value_of_parametr
        {
            get { return value_of_parametr; }

            set { value_of_parametr = value; }
        }

     private string name_of_parametr
        {
            get { return name_of_parametr; }

            set { name_of_parametr = value; }
        }

     private string name_of_sector
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
        public interactive_parser(string path)
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
                            if///

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


        } */
}

