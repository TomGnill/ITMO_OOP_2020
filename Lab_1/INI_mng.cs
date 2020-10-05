using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace CFG_mng
{
    public class InteractiveParser
    {
        Dictionary<ParamObj, string> _paramList = new Dictionary<ParamObj, string>();
        public string Pathfile { get; set; }

        private UInt16 _numstring;
        public string ValueOfParametr;
        public string NameOfParametr;
        public string NameOfSector;
        public  InteractiveParser(string catlog)
       {
            NameOfSector = null;
            Pathfile = catlog;
            _numstring = 0;
            ini_reader();
       }
        public struct ParamObj
        {
            public string ParamName;
            public string ParamSector;
            public string ParamValue;
        }
        void new_param()
        {
            var recordParam = new ParamObj
            {
                ParamName = NameOfParametr,
                ParamSector = NameOfSector,
                ParamValue = ValueOfParametr
            };
            _paramList.Add(recordParam, NameOfSector);
        }

        public void ParamList() 
        {
            foreach (var (key,value) in _paramList)
            {
                Console.WriteLine(key.ParamName + " сектор: " + key.ParamSector + " значение: " + key.ParamValue);
            }
            Console.WriteLine("Введите имя параметра, для поиска по имени или по значению, для выхода введите exit:");
            string key1 = Console.ReadLine();
            Findparam(key1);
        }


         public void Findparam(string enterKey)
        { 
            foreach (var (key,value) in _paramList)
            {
                if (key.ParamName == enterKey)
                {
                    Console.WriteLine($"Value = {key.ParamValue}");
                }

                if (key.ParamValue == enterKey)
                {
                    Console.WriteLine($"param name = {key.ParamName}");
                }

                if (enterKey == "exit")
                {
                    Environment.Exit(0);
                }
            }
            ParamList();
        }
        public bool string_reader(string fileString)
        {
            if (fileString.Trim().Length == 0)
                return false;

            if (fileString.StartsWith(" "))
                throw new Exception(" Некорректный формат строки " + Convert.ToString(_numstring));

            fileString = fileString.Trim();

            if (fileString.StartsWith("[") && fileString.EndsWith("]"))
            {
                var sectornameSubstring = fileString.Substring(1, fileString.Length - 2);
                var alphavet = Regex.Matches(sectornameSubstring, "[A-Z_]");
                if (alphavet.Count != sectornameSubstring.Length)
                {
                    throw new Exception("Имя раздела имеет нестандартный вид" + Convert.ToString(_numstring));
                }

                NameOfSector = sectornameSubstring;
                return false;
            }

            var comments = fileString.IndexOf(';');
            if (comments != -1)
            {
                fileString = fileString.Substring(0, comments);
            }

            var parline = fileString.Split(new[] { ' ', '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (parline.Length != 2)
            {
                throw new Exception("Некорректный формат параметра" + Convert.ToString(_numstring));
            }

            var alfavetCollection1 = Regex.Matches(parline[0], "[A-Za-z0-9_/]");
            var alfavetCollection2 = Regex.Matches(parline[1], "[A-Za-z0-9_/]");
            NameOfParametr = parline[0];
            ValueOfParametr = parline[1];
            return true;

        }

        public void ini_reader()
        {

            var fileInfo = new FileInfo(Pathfile);
            if (fileInfo.Exists && fileInfo.Extension == ".ini")
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Pathfile, System.Text.Encoding.Default))
                    { 
                        string fileString;
                       for (_numstring = 1; (fileString = sr.ReadLine()) != null; _numstring++)
                        {
                            if (string_reader(fileString))
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

