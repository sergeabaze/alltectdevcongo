using System;
using System.Collections.Generic;
using System.Threading;

namespace AllTech.FrameWork.Models
{
    public class CompanyModel
    {
        public int Revenue { get; set; }
        public string Name { get; set; }
        public List<ProductModel> Products { get; set; }

        //The sample data is generated in the constructor:
        public CompanyModel()
        {
            Products = new List<ProductModel>();
            Revenue = 0;

            ProductModel product;
            for (int i = 0; i < 10; i++)
            {
                product = new ProductModel();
                product.Name = "P" + i;
                Revenue += product.Revenue;
                Products.Add(product);
            }
        }
    }

    //Model class for the Product entries
    public class ProductModel
    {
        public int Revenue { get; set; }
        public string Name { get; set; }
        public List<SaleModel> Sales { get; set; }

        //The sample data is generated in the constructor:
        public ProductModel()
        {
            Sales = new List<SaleModel>();
            Revenue = 0;

            Random random = new Random();
            int rnd;
            for (int i = 0; i < 4; i++)
            {
                rnd = random.Next(0, 100);
                Revenue += rnd;
                Sales.Add(new SaleModel
                {
                    Value = rnd,
                    Quarter = "Quarter " + i
                });
            }
        }
    }

    //The model class for the Sale entries
    public class SaleModel
    {
        public int Value { get; set; }
        public string Quarter { get; set; }

        //This ensures that when calling rnd = random.Next(0, 100) in the ProductModel constructor
        //will return a different value (the random number is generated depending on the computer time)
        public SaleModel()
        {
            Thread.Sleep(1);
        }
    }
}
