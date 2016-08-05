using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebApp.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int Quantity { get; set; }
        public decimal SalePricePerUnit { get; set; }
        public decimal TotalCost { get; set; }

        public int ProductId { get; set; }
        public string UserId { get; set; }
    }
}