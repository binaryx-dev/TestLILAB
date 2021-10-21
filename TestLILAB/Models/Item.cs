using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestLILAB.Models
{
    public class Item
    {
        [Key]
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int ShopCartId { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public ShopCart Parent { get; set; }
        public Product Product { get; set; }
    }
}