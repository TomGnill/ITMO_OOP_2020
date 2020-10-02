using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace CFG_mng
{
    public class interactive_parser
    {
        Dictionary<param_obj, string> param_list = new Dictionary<param_obj, string>();
        string catlog = @"C:\Users\Андрейка\source\repos\OOP_LABS_AD\Lab_1\config_test.ini";
        public string Pathfile { get; set; }

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

       public void ini_open(string path)
        {
            Pathfile = path;
            numstring = 0;
            ini_reader();
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
                    exit_interactive(y_n);
                    ;
                    break;
                default:
                    Console.WriteLine(" Вы выбрали несуществующее действие!");
                    break;

            }
        }

         static public void exit_interactive(char y_n)
        {
            switch (y_n)
            {
                case 'y': Environment.Exit(0);break;
                case 'n':inter_menu(func_num.open_file);
                    break;
                default: Console.WriteLine("Y/N?");
                    exit_interactive(y_n);break;
            }
            
        }

        public struct param_obj
        {
            public string param_name;
            public string param_sector;
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
        public bool string_reader(string file_string)
        {
            if (file_string.Trim().Length == 0)
                return false;

            if (file_string.StartsWith(" "))
                throw new Exception(" Некорректный формат строки " + Convert.ToString(numstring));

            file_string = file_string.Trim();

            if (file_string.StartsWith("[") && file_string.EndsWith("]"))
            {
                var sectornameSubstring = file_string.Substring(1, file_string.Length - 2);
                var alphavet = Regex.Matches(sectornameSubstring, "[A-Z_]");
                if (alphavet.Count != sectornameSubstring.Length)
                {
                    throw new Exception("Имя раздела имеет нестандартный вид" + Convert.ToString(numstring));
                }

                name_of_sector = sectornameSubstring;
                return false;
            }


            var parline = file_string.Split(new[] { ' ', '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (parline.Length != 2)
            {
                throw new Exception("Некорректный формат параметра" + Convert.ToString(numstring));
            }

            var alfavetCollection1 = Regex.Matches(parline[0], "[A-Za-z0-9_/]");
            var alfavetCollection2 = Regex.Matches(parline[1], "[A-Za-z0-9_/]");
            name_of_parametr = parline[0];
            value_of_parametr = parline[1];
            return true;

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
                        string file_string;
                        while ((file_string = sr.ReadLine()) != null)
                        {
                            numstring++;
                            if (string_reader(file_string))
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
}

