using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestLILAB.Models
{
    public class ShopCart
    {
        [Key]
        public int ID { get; set; }
        public double SubTotal { get; set; }
        public double IGV { get; set; }
        public double Total { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public List<Item> Items { get; set; }
    }
}
