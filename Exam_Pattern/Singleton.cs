using System;
using System.Collections.Generic;
using System.Text;

namespace Exam_Pattern
{
    
    class Singleton
    {
        private Singleton() { }

        private static Singleton _instance;

        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        
        public static void someBusinessLogic()
        {
        }
    }

    /*class Program
    {
        static void Main(string[] args)
        {
    
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

            if (s1 == s2)
            {
                Console.WriteLine("Singleton works, both variables contain the same instance.");
            }
            else
            {
                Console.WriteLine("Singleton failed, variables contain different instances.");
            }
        }
    }
    */
}
