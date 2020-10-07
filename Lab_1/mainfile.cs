
namespace CFG_mng
{   public class programm
    { 
        static void Main()
        {
            Parser parser1 = new Parser("C:/Users/Андрейка/source/repos/OOP_LABS_AD/Lab_1/config_test.ini");
            Interactive interactive1 = new Interactive(parser1);
        }
    }
}