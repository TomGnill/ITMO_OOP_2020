using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace CFG_mng
{
    public class Parser
    {
      public static Dictionary<ParamObj, string> ParamList = new Dictionary<ParamObj, string>();
        public string Pathfile { get; set; }

        private UInt16 _numstring;
        public string NameOfSector;
        public Parser(string catlog)
        {
            NameOfSector = null;
            Pathfile = catlog;
            _numstring = 0;
            OpenAndRead();
        }
        public struct ParamObj
        {
            public string ParamName;
            public string ParamSector;
            public string ParamValue;
        }
        void addObj(string NameParam, string ValueParam)
        {
            var recordParam = new ParamObj
            {
                ParamName = NameParam,
                ParamSector = NameOfSector,
                ParamValue = ValueParam
            };
            ParamList.Add(recordParam, NameOfSector);
        }

       public static void getInt(string value)                
         {
             int ParamValue = Convert.ToInt32(value);
             Console.WriteLine($"intValue = {ParamValue}");
         }
       public static void getDouble(string value)
         {
             Double ParamValue = Convert.ToDouble(value);
             Console.WriteLine($"doubleValue = {ParamValue}");
        }
       public static void getStr(string value)
         {
             string ParamValue = Convert.ToString(value);
             Console.WriteLine($"stringValue = {ParamValue}");
        }

        public bool ParseString(string fileString)//red
        {
            if (fileString.StartsWith("[") && fileString.EndsWith("]"))                                             //узнаём сектор
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
            var comments = fileString.IndexOf(';');                                                             //чистим комменты
            if (comments != -1)
            {
                fileString = fileString.Substring(0, comments);
            }
            if (fileString.StartsWith(" "))                                                                       
                throw new Exception(" Некорректный формат строки " + Convert.ToString(_numstring));
            fileString = fileString.Trim();

            if (fileString.Trim().Length == 0)
                return false;

            var parline = fileString.Split(new[] { ' ', '=' }, StringSplitOptions.RemoveEmptyEntries); //Добавляем параметры 
            if (parline.Length != 2) 
            {
                throw new Exception("Некорректный формат параметра" + Convert.ToString(_numstring));
            }
            Regex.Matches(parline[0], "[A-Za-z0-9_/]");
            Regex.Matches(parline[1], "[A-Za-z0-9_/]");
            addObj(parline[0],  parline[1]);
           return true;

        }

        public void OpenAndRead()
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
                            if (ParseString(fileString))
                            {
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

    public class Interactive
    {
        public Interactive(Parser parser)
        {
          SnowParam();
        }
        public void SnowParam() 
        {
            foreach (var (key, value) in Parser.ParamList)
            {
                Console.WriteLine(key.ParamName + " сектор: " + key.ParamSector + " значение: " + key.ParamValue);
            }
            Console.WriteLine("Введите имя параметра, для поиска по имени или по значению, для выхода введите exit:");

            string key1 = Console.ReadLine();
            Findparam(key1);
        }


        public void Findparam(string enterKey)//new class
        {
            foreach (var (key, value) in Parser.ParamList)
            {
                if (key.ParamName == enterKey)
                {

                    Parser.getInt(key.ParamValue);
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
            SnowParam();
        }

    }
}

