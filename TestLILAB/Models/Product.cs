using MySql.EntityFrameworkCore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestLILAB.Models
{
    [MySqlCharset("utf8")]
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public int ImageID { get; set; }
        public ImageObj Image { get; set; }
        public double Price { get; internal set; }
    }
}
