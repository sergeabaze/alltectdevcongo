using System;
using System.Collections.Generic;
using AllTech.FrameWork.Models;

namespace AllTech.FrameWork.Models
{
    public class Sale
    {
        public Sale()
        {
            this.Product = new Product();
            this.Seller = new Seller();
        }

        public Sale(Seller seller, Product product, DateTime dateTime)
        {
            this.Product = product;
            this.Seller = seller;
            this.Date = dateTime;
        }

        public Sale(Sale sale)
        {
            this.Product = sale.Product;
            this.Seller = sale.Seller;
            this.Date = sale.Date;
            this.Value = sale.Value;
            this.Quarter = sale.Quarter;
        }

        public Product Product { get; set; }
        public Seller Seller { get; set; }
        public DateTime Date { get; set; }

        public double Value { get; set; }
        public string Quarter { get; set; }
        
        public string City
        {
            get { return Seller.City; }
            set { Seller.City = value; }
        }

        public int NumberOfUnits { get; set; }

        public double AmountOfSale
        {
            get { return UnitPrice * NumberOfUnits; }
            set { Product.UnitPrice = value / NumberOfUnits; }
        }

        public double UnitPrice
        {
            get { return Product.UnitPrice; }
            set { Product.UnitPrice = value; }
        }

        public Sale Clone()
        {
            return new Sale(this);
        } 
    }
}