﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace CFG_mng
{
    public class interactive_parser
    {
        string catlog = @"C:\Users\Андрейка\source\repos\OOP_LABS_AD\Lab_1\config_test.ini";
        Dictionary<param_obj, string> param_list = new Dictionary<param_obj, string>();
        public string Pathfile { get; set; }

        private UInt16 numstring;
        public string value_of_parametr;
        public string name_of_parametr;
        public string name_of_sector;
        public  interactive_parser(string catlog)
       {
            name_of_sector = null;
            Pathfile = catlog;
            numstring = 0;
            ini_reader();
       }
        public struct param_obj
        {
            public string param_name;
            public string param_sector;
            public string param_value;
        }
        void new_param()
        {
            var record_param = new param_obj
            {
                param_name = name_of_parametr,
                param_sector = name_of_sector,
                param_value = value_of_parametr
            };
            param_list.Add(record_param, name_of_sector);
        }

        public void ParamList() 
        {
            foreach (var (key,value) in param_list)
            {
                Console.WriteLine(key.param_name + " - " + key.param_sector + " - " + key.param_value);
            }
            Console.WriteLine("Введите имя параметра, для поиска по имени или по значению, для выхода введите exit:");
            string key1 = Console.ReadLine();
            findparam(key1);
        }


         public void findparam(string enter_key)
        { 
            foreach (var (key,value) in param_list)
            {
                if (key.param_name == enter_key)
                {
                    Console.WriteLine($"Value = {key.param_value}");
                }

                if (key.param_value == enter_key)
                {
                    Console.WriteLine($"param name = {key.param_name}");
                }

                if (enter_key == "exit")
                {
                    Environment.Exit(0);
                }
            }
            ParamList();
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

            var comments = file_string.IndexOf(';');
            if (comments != -1)
            {
                file_string = file_string.Substring(0, comments);
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
                        ParamList();
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

