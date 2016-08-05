using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryWebApp.Models
{
    public class Restock
    {
        public int RestockId { get; set; }

        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public string UserId { get; set; }
    }
}