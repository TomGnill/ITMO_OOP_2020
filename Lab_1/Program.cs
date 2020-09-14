using System;
using System.Collections.Generic;
using System.IO;


namespace CFG_mng
{
    class cfg_obj
    {
        public string cfg_name;
        public int cfg_value;
        public string cfg_glname;
        public UInt16 id_par;
        public cfg_obj(string gl_name, string name, int value, UInt16 id)//конструктор 
        {
            cfg_glname = gl_name;
            cfg_name = name;
            cfg_value = value;
            id_par = id;
        }

        public void GetInfo()
        {
            Console.WriteLine($"Раздел: {cfg_glname} параметр: {cfg_name} значение параметра: {cfg_value}");// полный вывод информации о параметре
        }

        public void GetValue()
        {
            Console.WriteLine($"Значение параметра: {cfg_value}"); //узнаём значение по ключу(им может быть имя параметра)
        }


        public void GetName(string gl_name)
        {
            if (gl_name == cfg_glname)
            {
                for (int i = 0; i < id_par; i++)
                {
                    Console.WriteLine($"Список параметров в группе: \n {cfg_name}");
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            char open_cat = '[', close_cat = ']', value_indecator = '=';
            
            string catlog = @"C:\Users\Андрейка\source\repos\OOP_LABS_AD\Lab_1\config_test.ini";
            using (StreamReader sr = new StreamReader(catlog, System.Text.Encoding.Default))
            {
                if(textLine.star)
            }
         

        }
    }

    
    
}
