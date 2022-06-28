using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.FrameWork.Models
{
    public class Manufacturer
    {
        public string Name { get; set; }
        public int Revenue { get; set; }
        public int ProductsCount { get { return Products.Count; } }

        public List<InventoryProduct> Products { get; set; }

        public Manufacturer(string name, string revenue, List<InventoryProduct> products)
        {
            Name = name;
            Revenue = Int32.Parse(revenue);
            Products = products;
        }
    }

    public class InventoryProduct
    {
        public string Name { get; set; }
        public double StandardCost { get; set; }
        public int InventoryCount { get { return InventoryEntries.Sum(entry => entry.Quantity); } }

        public List<InventoryEntry> InventoryEntries { get; set; }

        public InventoryProduct(string name, string standardCost, List<InventoryEntry> inventoryEntries)
        {
            Name = name;
            StandardCost = Double.Parse(standardCost, System.Globalization.CultureInfo.InvariantCulture);
            InventoryEntries = inventoryEntries;
        }
    }

    public class InventoryEntry
    {
        public string Shelf { get; set; }
        public int Quantity { get; set; }

        public InventoryEntry(string shelf, string quantity)
        {
            Shelf = shelf;
            Quantity = Int32.Parse(quantity);
        }
    }
}
