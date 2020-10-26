using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ShopsAssist
{
    public class Shop
    {
        public int ShopId;
        public string ShopName;
        public string Adress;
        public static string SupplyList { get; set; }
        public List<Product> Products;

        public Shop(int id, string name, string adr, string supplyList11)
        {
            ShopId = id;
            ShopName = name;
            Adress = adr;
            SupplyList = supplyList11;
            Products = SetPriceList();
        }

        public struct Product
        {
            public string ProductName;
            public int ProductId;
            public int ProductQuantity;
            public int ProductPrice;
        }

        public static Product AddProduct(string prodName, int prodId, int prodQ, int prodP) //метод создать продукт
        {
            Product newProduct = new Product
            {
                ProductName = prodName,
                ProductId = prodId,
                ProductQuantity = prodQ,
                ProductPrice = prodP
            };

            return newProduct;

        }

        public static List<Product> SetPriceList()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<Product> priceList1 = new List<Product>();
            Product newProduct;
          

            byte[] bin = File.ReadAllBytes("C:/Users/Андрейка/source/repos/TomGnill/ITMO_OOP_2020/ShopsAssist/PriceList's/PriceList.xlsx");

            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[SupplyList];


                var row = 1;
                while (row < firstWorksheet.Dimension.End.Row)
                {
                    row++;
                    string sProdId = firstWorksheet.Cells[row, 1].Value.ToString();
                    string prodName = firstWorksheet.Cells[row, 2].Value.ToString();
                    string sProdPrice = firstWorksheet.Cells[row, 3].Value.ToString();
                    string sProdq = firstWorksheet.Cells[row, 4].Value.ToString();

                    int prodId = Convert.ToInt32(sProdId);
                    int prodPrice = Convert.ToInt32(sProdPrice);
                    int prodQ = Convert.ToInt32(sProdq);
                    newProduct = AddProduct(prodName, prodId, prodQ, prodPrice);
                    priceList1.Add(newProduct);

                }
                excelPackage.Save();
            } 
          
            return priceList1;
        }

        public static void ShowMagazinesList()
        {
            foreach (Shop magazine in Starter.MagazinesList)
            {
                Console.WriteLine("//Уникальный номер:" + magazine.ShopId + "// Имя магазина: " + magazine.ShopName +
                                  " //Адрес: " +
                                  magazine.Adress);
            }
        }


        public static void ShowPriceList(int id)
        {
            foreach (var aProduct in Starter.MagazinesList.Where(magazine => magazine.ShopId == id).SelectMany(magazine => magazine.Products))
            {
                Console.WriteLine("//Уникальный номер:" + aProduct.ProductId + "// Имя продукта " + aProduct.ProductName + " //Цена: " + aProduct.ProductPrice + "//Количество :" + aProduct.ProductQuantity);
            }
        }

        public static void EditPrice(int shopId, string prodName, int newPrice)
        {
            int saveID;
            string saveName;
            int saveQ;
            int index = 0;
            Product newPriceProduct;
            foreach (var magazine in Starter.MagazinesList.Where(magazine => magazine.ShopId == shopId))
            {
                foreach (Product p in magazine.Products)
                {
                    if (p.ProductName == prodName)
                    {
                        saveID = p.ProductId;
                        saveName = p.ProductName;
                        saveQ = p.ProductQuantity;

                        newPriceProduct = new Product()
                        {
                            ProductPrice = newPrice,
                            ProductId = saveID,
                            ProductQuantity = saveQ,
                            ProductName = saveName
                        };
                        magazine.Products[index] = newPriceProduct;
                        break;
                    }
                    index++;
                }
            }
        }

        public static List<string> Bomj(int id, int wallet)
        {
            List<string> testList = new List<string>();
            int sum = 0, que = 0;
            var shoplist = Starter.MagazinesList.ElementAt(id - 1);
            var productlist = shoplist.Products.Count;
            Console.WriteLine($"{productlist}");

            var selectedProd = from prod in shoplist.Products
                               where prod.ProductPrice < wallet
                               select prod;
            foreach (Product product in selectedProd)
            {

                int max = product.ProductQuantity;
                que = wallet / product.ProductPrice;
                sum = que * product.ProductPrice;
                if (que <= max)
                {
                    Console.WriteLine($"можно купить {product.ProductName}, на сумму:{sum}, в количестве: {que}");
                    testList.Add($"можно купить {product.ProductName}, на сумму:{sum}, в количестве: {que}");
                }

            }

            return testList;

        }

        public static int CalcLot(int shopId, int prodId, int prodQ)
        {
            int sum = 0;
            foreach (var magazine in Starter.MagazinesList.Where(magazine => magazine.ShopId == shopId))
            {
                foreach (Product p in magazine.Products)
                {
                    if (p.ProductId != prodId) continue;
                    if (prodQ <= p.ProductQuantity)
                    {

                        sum = prodQ * p.ProductPrice;

                    }
                    else
                    {
                        throw new Exception("В магазине нет столько товаров");
                    }
                }

                Console.WriteLine($"Сумма покупки: {sum}");
            }

            return sum;
        }

        public static int AloneBuyHelp(int prodId)
        {
            int savePrice = 999999;
            int iterator = 0;
            string adr = null;
            string name = null;

            int id = 0;
            foreach (Shop magazine in Starter.MagazinesList)
            {
                foreach (var price in magazine.Products.Where(price => price.ProductId == prodId).Where(price => savePrice > price.ProductPrice))
                {
                    savePrice = price.ProductPrice;
                    iterator++;
                }

                if (magazine.ShopId != iterator) continue;
                name = magazine.ShopName;
                id = magazine.ShopId;
                adr = magazine.Adress;
            }

            Console.WriteLine($"Дешевле в магазине : {name} ({id}), цена продукта : {savePrice}");
            Console.WriteLine($"Адрес магазина : {adr}");
            return savePrice;

        }

        public static int ListBuyHelp(List<(string, int)> userList)

        {
            List<int> chek = new List<int>();
            int chekSumm = userList.Count;
            foreach (Shop magazine in Starter.MagazinesList)
            {
                int sum = 0;

                int count = 0;
                var itemList = from item in userList
                               select item;
                foreach (Product product in magazine.Products)
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

                if (chekSumm != count) continue;
                chek.Add(sum);
                sum = 0;

            }

            Console.WriteLine($"Сумма покупки:{chek.Min()}");
            return chek.Min();
        }
    }
}
