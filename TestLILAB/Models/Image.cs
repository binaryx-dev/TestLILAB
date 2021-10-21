using MySql.EntityFrameworkCore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestLILAB.Models
{
    [MySqlCharset("utf8")]
    public class ImageObj
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
