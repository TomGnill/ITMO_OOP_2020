using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using ShopsAssist;


namespace ShopsAssist
{
    public class Shop
    {

        public struct Magazine
        {
            public int ShopID;
            public string ShopName;
            public string Adress;
        }
        public static Dictionary<SetPrice.Product, int> PriceList = new Dictionary<SetPrice.Product, int>();

        public static void addMagazine(int ID, string Name, string adr)
        {
            var newMagazine = new Magazine
            {
                ShopID = ID,
                ShopName = Name,
                Adress = adr,
            };
            var NewPriceList = new Dictionary<SetPrice.Product, int>();

            Starter.MagazinesList.Add(newMagazine, NewPriceList);
        }

        public static void addThreeShops() //тестовое добаление магазинов 
        {
            addMagazine(121, "Пятёрочка", "Бульвар 4");
            addMagazine(142, "Магнит", "Соседний бульвар 4");
            addMagazine(521, "Обрыгаловка", "Параллельный бульвар 4");
        }

    }

    public class SetPrice : Shop
    {

        public string Pathfile { get; set; }
        private UInt16 numstring;
        public int ProductPrice;

        public SetPrice(string catlog)
        {
            numstring = 0;
            Pathfile = catlog;
            OpenList();
        }

        public void OpenList()
        {
            var fileInfo = new FileInfo(Pathfile);
            if (fileInfo.Exists && fileInfo.Extension == ".txt")
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Pathfile, System.Text.Encoding.Default))
                    {
                        string fileString;
                        for (numstring = 1; (fileString = sr.ReadLine()) != null; numstring++)
                        {
                            if (SetShopPrice(fileString))
                            {
                                ProductPrice = 0;
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

        public bool SetShopPrice(string filestring)
        {
            filestring = filestring.Trim();
            if (filestring.Trim().Length == 0)
                return false;

            var parline = filestring.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
            if (parline.Length != 3)
            {
                throw new Exception("Некорректный формат параметра" + Convert.ToString(numstring));
            }

            Regex.Matches(parline[0], "[A-Za-z0-9_/]");
            Regex.Matches(parline[1], "[A-Za-z0-9_/]");
            int ProdID = Convert.ToInt32(parline[1]);
            Regex.Matches(parline[2], "[A-Za-z0-9_/]");
            int ProdQ = Convert.ToInt32(parline[2]);
            addProduct(parline[0], ProdID, ProdQ);


            Console.WriteLine($"Введите цену для продукта {parline[0]} :");
            int IPrice = Console.Read();
            ProductPrice = IPrice;
            return true;
        }

        public struct Product
        {
            public string ProductName;
            public int ProductID;
            public int ProductQuantity;
        }

        public void addProduct(string ProdName, int ProdID, int ProdQ)
        {

            var newProduct = new Product
            {
                ProductName = ProdName,
                ProductID = ProdID,
                ProductQuantity = ProdQ
            };
            PriceList.Add(newProduct, ProductPrice);
        }
    }

    public class MenusFitues
    {
    public static void ShowMagazinesList()
        {
            foreach (var (key, value) in Starter.MagazinesList)
            {
                Console.WriteLine("Уникальный номер:" + key.ShopID + " Имя магазина: " + key.ShopName + " Адрес: " + key.Adress);
            }
        }
    }
}
    

