using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using OfficeOpenXml;



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

        public static void AddPriceList(string catlog, List<Product> aList)
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
                Console.WriteLine("//Уникальный номер:" + key.ShopID + "// Имя магазина: " + key.ShopName +
                                  " //Адрес: " +
                                  key.Adress);
            }
        }

        public static void ShowPriceList(int ID)
        {
            foreach (var (key, value) in Starter.MagazinesList)
            {
                if (key.ShopID == ID)
                {
                    foreach (Shop.Product aProduct in value)
                    {
                        Console.WriteLine("//Уникальный номер:" + aProduct.ProductID + "// Имя продукта " +
                                          aProduct.ProductName + " //Цена: " +
                                          aProduct.ProductPrice + "//Количество :" + aProduct.ProductQuantity);
                    }
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

        public static List<string> Bomj(int ID, int wallet)
        {
            List<string> testList = new List<string>();
            int sum = 0, que = 0;
            var shoplist = Starter.MagazinesList.ElementAt(ID - 1);
            var productlist = shoplist.Value.Count;
            Console.WriteLine($"{productlist}");

            var selectedProd = from prod in shoplist.Value
                where prod.ProductPrice < wallet
                select prod;
            foreach (Shop.Product product in selectedProd)
            {

                int max = product.getQuantity();
                que = wallet / product.ProductPrice;
                sum = que * product.ProductPrice;
                if (que <= max)
                {
                    Console.WriteLine($"можно купить {product.getName()}, на сумму:{sum}, в количестве: {que}");
                    testList.Add($"можно купить {product.getName()}, на сумму:{sum}, в количестве: {que}");
                }

            }

            return testList;

        }

        public static int CalcLot(int ShopID, int ProdID, int ProdQ)
        {
            int sum = 0;
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

            return sum;
        }

        public static int AloneBuyHelp(int prodID)
        {
            int savePrice = 999999;
            int iterator = 0;
            string Adr = null;
            string name = null;
            int id = 0;
            foreach (KeyValuePair<Shop.Magazine, List<Shop.Product>> kvp in Starter.MagazinesList)
            {

                foreach (Shop.Product price in kvp.Value)
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

                if (kvp.Key.ShopID == iterator)
                {
                    name = kvp.Key.ShopName;
                    id = kvp.Key.ShopID;
                    Adr = kvp.Key.Adress;
                }
            }

            Console.WriteLine($"Дешевле в магазине : {name} ({id}), цена продукта : {savePrice}");
            Console.WriteLine($"Адрес магазина : {Adr}");
            return savePrice;

        }

        public static int ListBuyHelp(List<(string, int)> userList)

        {
            List<int> chek = new List<int>();
            int chekSumm = userList.Count;
            string Adress = null;
            string Name = null;
            foreach (var (key, value) in Starter.MagazinesList)
            {
                int sum = 0;
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
                    sum = 0;
                }

            }

            Console.WriteLine($"Сумма покупки:{chek.Min()}");
            return chek.Min();
        }

    }

    public class ExprotExcel
    { 
        public static void ExportExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "TomGnill";
                excelPackage.Workbook.Properties.Title = "PriceList Shop1";
                excelPackage.Workbook.Properties.Subject = "EPPlus demo export data";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

              
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells[1, 1].Value = "Product ID";
                worksheet.Cells[1, 2].Value = "Product Name";
                worksheet.Cells[1, 3].Value = "Product Price";
                worksheet.Cells[1, 4].Value = "Product Quantity";
                var row = 1;
                foreach (var (key, value) in Starter.MagazinesList)
                {
                    if (key.ShopID == 1)
                    {
                        foreach (Shop.Product product in value)
                        {
                            row++;
                            worksheet.Cells[row, 1].Value = product.ProductID;
                            worksheet.Cells[row, 2].Value = product.ProductName;
                            worksheet.Cells[row, 3].Value = product.ProductPrice;
                            worksheet.Cells[row, 4].Value = product.ProductQuantity;


                        }
                    }
                }

                FileInfo fi =
                    new FileInfo(
                        @"C:\Users\Андрейка\source\repos\TomGnill\ITMO_OOP_2020\ShopsAssist\PriceList's\PriceList1.xlsx");
                excelPackage.SaveAs(fi);

            }
        }


    }
}