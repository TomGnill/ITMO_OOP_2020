using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;



namespace ShopsAssist
{
    public class Shop
    {
       
        public Shop(int ID, string Name, string adr, string SupplyList) 
        {
            var newMagazine = new Magazine
            {
                ShopID = ID,
                ShopName = Name,
                Adress = adr,
            }; 
           
            SetPriceList(SupplyList);
                Starter.MagazinesList.Add(newMagazine, newPriceList);
        }

        public static List<Product> newPriceList = new List<Product>();

        public struct Magazine
        {
            public int ShopID;
            public string ShopName;
            public string Adress;

            public int getID()
            {
                return ShopID;
            }
            public string getName()
            {
                return ShopName;
            }
            public string  getAdr()
            {
                return Adress;
            }
        }

        public struct Product
        {
            public string ProductName;

           
            public int ProductID;
            public int ProductQuantity;
            public int ProductPrice;

            public int getID()
            {
                return ProductID;
            }
            public string getName()
            {
                return ProductName;
            }
            public void editQuantity(int ProdQ)

            {
                ProductQuantity += ProdQ;
            }

            public int getQuantity()
            {
                return ProductQuantity;
            }
            public void editPrice(int ProdP)
            {
                ProductPrice = ProdP;
            }
            public int getPrice()
            {
                return ProductPrice;
            }
        }
        public static void addProduct(string ProdName, int ProdID, int ProdQ, int ProdP) //метод создать продукт
        {
            Product newProduct = new Product
            {
                ProductName = ProdName,
                ProductID = ProdID,
                ProductQuantity = ProdQ,
                ProductPrice = ProdP
            };
            newPriceList.Add(newProduct);

        }
        public static string Pathfile { get; set; }
        private static UInt16 numstring;

        public static void SetPriceList(string catlog)
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

            var parline = filestring.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);

            if (parline.Length != 4)
            {
                throw new Exception("Некорректный формат параметра" + Convert.ToString(numstring));
            }

            Regex.Matches(parline[0], "[A-Za-z0-9_/]");

            Regex.Matches(parline[1], "[A-Za-z0-9_/]");

            int ProdID = Convert.ToInt32(parline[1]);

           

            Regex.Matches(parline[2], "[A-Za-z0-9_/]");

            int ProdQ = Convert.ToInt32(parline[2]);

            foreach (Product p in new List<Product>())
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
                   Shop.SetPriceList(catlog);

                }
               
            }

        }

        public static void editPrice(int shopID, string ProdName, int newPrice)
        {
            foreach (var (key, value) in Starter.MagazinesList)
            {
                if (key.ShopID == shopID)
                {
                    foreach (Shop.Product p in value)
                    {
                        if (p.ProductName == ProdName)
                        {
                            p.editPrice(newPrice);
                        }
                        else
                        {
                            throw new Exception("Такого товара в магазине нет");
                        }
                    }
                }
                else
                {
                    throw new Exception("Такого магазина  нет");
                }
            }
        }

        public static void Bomj(int ID, int wallet)
        {
            foreach (var (key, value) in Starter.MagazinesList)
            {
                if (key.ShopID == ID)
                {
                    foreach (Shop.Product p in value)
                    {
                        for (int i = 100; i < p.getID(); i++)
                        {
                            string Name = p.getName();
                            int price = p.getPrice();
                            int max = p.getQuantity();
                            for (int Quantity = 0; Quantity < max; Quantity++)
                            {
                                int sum = price * Quantity;
                                if ((sum > wallet) && (Quantity == 1))
                                {
                                    Console.WriteLine($"В этом магазине вы не сможете купить {Name}, так как цена за одну штуку составляет {price}"); break;
                                    
                                }
                                if (sum > wallet)
                                {
                                    sum -= price;
                                    Console.WriteLine($"Можно купить {Name} , в количестве {Quantity}, на сумму {sum}");
                                    break;
                                }

                            }


                        }

                    }
                }
            }
        }

        public static void CalcLot(int ShopID, int ProdID, int ProdQ)
        {
            foreach (KeyValuePair<Shop.Magazine, List<Shop.Product>> kvp in Starter.MagazinesList)
            {
                if (kvp.Key.ShopID == ShopID)
                {
                    foreach (Shop.Product p in kvp.Value)
                    {
                        if (p.ProductID == ProdID)
                        {
                            if (ProdQ <= p.ProductQuantity)
                            {
                                int sum = ProdQ * p.ProductPrice;
                                Console.WriteLine($"Сумма покупки: {sum}");
                            }
                            else
                            {
                                throw new Exception("В магазине нет столько товаров");
                            }
                        }
                    }
                }
                
            }

        }

        public static void AloneBuyHelp(int prodID)
        {
            int savePrice = 999999;
            int saveID = 0;
            string savdName = null;
            string saveAdr = null;
            foreach (KeyValuePair<Shop.Magazine, List<Shop.Product>> kvp in Starter.MagazinesList)
            {
                foreach (Shop.Product price in kvp.Value)
                {
                    if (price.ProductID == prodID)
                    {
                        if (savePrice > price.ProductPrice)
                        {
                            
                            savePrice = price.ProductPrice;
                            
                            savdName = kvp.Key.getName();
                            saveID = kvp.Key.getID();
                            saveAdr = kvp.Key.getAdr();
                            Console.WriteLine($"{saveID}, {savePrice}");
                        }
                    }
                }
            }
            Console.WriteLine($"Дешевле в магазине : {savdName} ({saveID}), цена продукта : {savePrice}");
            Console.WriteLine($"Адрес магазина : {saveAdr}");

        }
        //public static void ListBuyHelp()
    }
}

