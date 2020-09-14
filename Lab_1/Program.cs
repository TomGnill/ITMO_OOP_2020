using System;
using System.Collections.Generic;
using System.IO;


namespace CFG_mng
{
    class Program
    {
        static void Main(string[] args)
        {
            char open_cat = '[', close_cat = ']', value_indecator = '=';
            

            
            string catlog = @"C:\Users\Андрейка\source\repos\OOP_LABS_AD\Lab_1\config_test.ini";
            using (StreamReader sr = new StreamReader(catlog, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    for(int i=0; i<line.length; i ++)
                    Console.WriteLine(line);
                }
            }
         

        }
    }
}
