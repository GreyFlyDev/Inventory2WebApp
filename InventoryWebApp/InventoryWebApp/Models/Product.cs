using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal SalePricePerUnit { get; set; }
        public decimal PurchasePricePerUnit { get; set; }
        public decimal TotalInvestment { get; set; }
        public int NumberOfSales { get; set; }
        public DateTime DateAdded { get; set; }

        public string UserId { get; set; }

    }
}