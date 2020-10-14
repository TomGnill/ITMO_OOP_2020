using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;


namespace ShopsAssist
{
    public class Shop
    {
        public Shop(int ID, string Name, string adr, string SupplyList) //метод создать магазин
        {
            var newMagazine = new Magazine
            {
                ShopID = ID,
                ShopName = Name,
                Adress = adr,
            };
            SetPriceList(SupplyList); //метод завоза партии товаров в магазин(цены назначаются отдельно для каждого товара)
            Starter.MagazinesList.Add(newMagazine, newPriceList);
        }

        public static List<Product> newPriceList = new List<Product>();

        public struct Magazine
        {
            public int ShopID;
            public string ShopName;
            public string Adress;
        }

        public struct Product
        {
            public string ProductName;
            public int ProductID;
            public int ProductQuantity;
            public int ProductPrice;
            public void editQuantity(int ProdQ)

            {
                ProductQuantity += ProdQ;
            }
            public void editPrice(int ProdP)
            {
                ProductPrice = ProdP;
            }
        }

        public static void addMagazine(int ID, string Name, string adr)
        {
            var newMagazine = new Magazine
            {
                ShopID = ID,
                ShopName = Name,
                Adress = adr,
            };
            Starter.MagazinesList.Add(newMagazine, newPriceList);
        }

        public static void addProduct(string ProdName, int ProdID, int ProdQ, int ProdP) //метод создать продукт
        {
            Console.WriteLine($"Введите цену для продукта {ProdName} :");
            string price = Console.ReadLine();
            int ProductPrice = Convert.ToInt32(price);

            Product newProduct = new Product
            {
                ProductName = ProdName,
                ProductID = ProdID,
                ProductQuantity = ProdQ,
                ProductPrice = ProdP
                
            };
            newPriceList.Add(newProduct);

        }

        public static void addThreeShops() //тестовое добаление магазинов 
        {
            //addMagazine(121, "Пятёрочка", "Бульвар 4", "C:/Users/Андрейка/Source/Repos/TomGnill/ITMO_OOP_2020/ShopsAssist/SupplyList2.txt");
            //addMagazine(142, "Магнит", "Соседний бульвар 4");
            //addMagazine(521, "Обрыгаловка", "Параллельный бульвар 4");
        }

        public static string Pathfile { get; set; }
        private static UInt16 numstring;
        public static int ProductPrice;

        public void SetPriceList(string catlog)
        {
            numstring = 0;
            Pathfile = catlog;
            OpenList();
        }

        public static void OpenList()
        {
            var fileInfo = new FileInfo(Pathfile);
            if (fileInfo.Exists && fileInfo.Extension == ".txt")
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Pathfile, System.Text.Encoding.Default))
                    {
                        string fileString;
                        for (numstring = 1; (fileString = sr.ReadLine()) != null;)
                        {
                            if (SetShopPrice(fileString))
                            {
                                numstring++;
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

        public static bool SetShopPrice(string filestring)
        {

            filestring = filestring.Trim();

            if (filestring.Trim().Length == 0)
            {
                return false;
            }

            var parline = filestring.Split(new[] {'.', '.'}, StringSplitOptions.RemoveEmptyEntries);

            if (parline.Length != 4)
            {
                throw new Exception("Некорректный формат параметра" + Convert.ToString(numstring));
            }

            Regex.Matches(parline[0], "[A-Za-z0-9_/]");

            Regex.Matches(parline[1], "[A-Za-z0-9_/]");

            int ProdID = Convert.ToInt32(parline[1]);

           

            Regex.Matches(parline[2], "[A-Za-z0-9_/]");

            int ProdQ = Convert.ToInt32(parline[2]);

            foreach (Product p in newPriceList)
            {
                if (p.ProductID == ProdID)
                {
                    p.editQuantity(ProdQ);
                    return true;
                }

            }

            Regex.Matches(parline[3], "[A-Za-z0-9_/]");

            int ProdP = Convert.ToInt32(parline[3]);

            addProduct(parline[0], ProdID, ProdQ, ProdP);

            return true;
        }
    }

    public class MenusFitues
    {
        public static void ShowMagazinesList()
        {
            foreach (var (key, value) in Starter.MagazinesList)
            {
                Console.WriteLine("Уникальный номер:" + key.ShopID + " Имя магазина: " + key.ShopName + " Адрес: " +
                                  key.Adress);
            }
        }

        
        public static void addSupplyList(int ID, string catlog)
        {
            foreach (var (key, value) in Starter.MagazinesList)
            {
                if (key.ShopID == ID)
                {
                    Shop.SetShopPrice(catlog);

                }
            }

        }

        public static void editPrice(int shopID, string ProdName, int newPrice)
        {
            foreach (var (key,value) in Starter.MagazinesList)
            {
                if (key.ShopID == shopID)
                {
                    foreach (Shop.Product p in Shop.newPriceList)
                    {
                        if (p.ProductName == ProdName)
                        {
                            p.editPrice(newPrice);
                        }
                    }
                }
            }
        }
        
    }
}

