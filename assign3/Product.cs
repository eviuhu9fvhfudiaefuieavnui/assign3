using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assign3
{
    internal class Product
    {
        // These names must be used in the DataPropertyName of the DataGridView columns
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        // This is the constructor that builds a Product object
        public Product(int id, string name, string brand, decimal price, int quantity)
        {
            ProductID = id;
            ProductName = name;
            ProductBrand = brand;
            ProductPrice = price;
            ProductQuantity = quantity;
        }
    }

}   
