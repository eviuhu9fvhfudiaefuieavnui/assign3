using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assign3
{
    // Represents one product line in an order (e.g. 1x Nike Air Max)
    public class OrderItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        // This calculates itself automatically - price x quantity
        public decimal Subtotal => UnitPrice * Quantity;
    }

    // Represents a full customer order which contains a list of OrderItems
    public class Order
    {
        public string OrderName { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }

        // A list that holds all the items in this order
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalAmount { get; set; }
    }
}