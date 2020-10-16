using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Product> newPriceList1 = new List<Product>(newPriceList);
            Starter.MagazinesList.Add(newMagazine, newPriceList1);
           
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

            public string getAdr()
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
            newPriceList.Clear();
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
            var shoplist = Starter.MagazinesList.ElementAt(ID-1);
            var productlist = shoplist.Value.Count;
            Console.WriteLine($"{productlist}");

            var selectedProd = from prod in shoplist.Value
                    where prod.ProductPrice < wallet
                    select prod;
                foreach (Shop.Product product in selectedProd)
                {
                    int sum, que;
                    int max = product.getQuantity();
                    que = wallet / product.ProductPrice;
                    sum = que * product.ProductPrice;
                    if (que <= max )
                    {
                        Console.WriteLine(
                            $"можно купить {product.getName()}, на сумму:{sum}, в количестве: {que}");
                    }

                }

            
        }

        public static void CalcLot(int ShopID, int ProdID, int ProdQ)
        {
            foreach (KeyValuePair<Shop.Magazine, List<Shop.Product>> kvp in Starter.MagazinesList)
            {
                if (kvp.Key.ShopID == ShopID)
                {
                    int sum = 0;
                    foreach (Shop.Product p in kvp.Value)
                    {
                        if (p.ProductID == ProdID)
                        {
                            if (ProdQ <= p.ProductQuantity)
                            {

                                 sum = ProdQ * p.ProductPrice;
                               
                            }
                            else
                            {
                                throw new Exception("В магазине нет столько товаров");
                            }
                        }
                    }
                    Console.WriteLine($"Сумма покупки: {sum}");
                }

            }

        }

        public static void AloneBuyHelp(int prodID)
        {
            int savePrice = 999999;
            int iterator = 0;
            foreach (var (key, value) in Starter.MagazinesList)
            {
                foreach (Shop.Product price in value)
                {
                    if (price.ProductID == prodID)
                    {

                        if (savePrice > price.ProductPrice)
                        {
                            savePrice = price.ProductPrice;
                            iterator++;
                        }
                    }
                }

                if (key.ShopID == iterator)
                {
                    Console.WriteLine(
                        $"Дешевле в магазине : {key.ShopName} ({key.ShopID}), цена продукта : {savePrice}");
                    Console.WriteLine($"Адрес магазина : {key.Adress}");

                }
            }
        }

        public static void ListBuyHelp(List<(string,int)> userList)

        {
                List< int> chek = new List<int>();
                int sum = 0;
                int chekSumm = userList.Count;
                string Adress = null;
                string Name = null;
                foreach (var (key , value) in Starter.MagazinesList)
                {
                string shopName = null;
                int count = 0;
                    var itemList = from item in userList
                    select item;
                    foreach (Shop.Product product in value)
                    {
                        foreach (var item in itemList)
                        {
                            int itemSum;
                            if (item.Item1 == product.ProductName & item.Item2 <= product.ProductQuantity)
                            {
                                itemSum = item.Item2 * product.ProductPrice;
                                sum += itemSum;
                                count++;
                            }

                        }
                    }

                   if (chekSumm == count)
                   {
                    chek.Add(sum);
                   }
                }
          
            Console.WriteLine($"Сумма покупки:{chek.Min()}");
        }
        
    }
}
