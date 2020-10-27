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
        public List<Product> Products;

        public Shop(int id, string name, string adr, string supplyList)
        {
            ShopId = id;
            ShopName = name;
            Adress = adr; 
            AddPriceList newPriceList = new AddPriceList();
            Products = newPriceList.SetPriceList(supplyList);
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

        public static List<(int, int)> Bomj(int id, int wallet)
        {
            List<(int,int)> useList = new List<(int, int)>();
            int sum = 0, que = 0;
            var shoplist = Starter.MagazinesList.ElementAt(id - 1);

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
                    useList.Add((product.ProductId, que));
                }

            }
            return useList;
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
            }

            return sum;
        }

        public static (int, int) LowestCost(int prodId)
        {
            int savePrice = 999999;
            (int, int) pair;

            int id = 0;
            foreach (Shop magazine in Starter.MagazinesList)
            {
                foreach (var price in magazine.Products.Where(price => price.ProductId == prodId).Where(price => savePrice > price.ProductPrice))
                {
                    savePrice = price.ProductPrice;
                    id = magazine.ShopId;
                }
            }
            pair = (id, savePrice);
            return pair;
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
            return chek.Min();
        }
    }

    public class AddPriceList
    {
        public List<Shop.Product> SetPriceList(string Supplylist)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<Shop.Product> priceList1 = new List<Shop.Product>();
            Shop.Product newProduct;


            byte[] bin = File.ReadAllBytes("C:/Users/Андрейка/source/repos/TomGnill/ITMO_OOP_2020/ShopsAssist/PriceList's/PriceList.xlsx");

            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[Supplylist];


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
                    newProduct = Shop.AddProduct(prodName, prodId, prodQ, prodPrice);
                    priceList1.Add(newProduct);

                }
                excelPackage.Save();
            }

            return priceList1;
        }

    }
}
