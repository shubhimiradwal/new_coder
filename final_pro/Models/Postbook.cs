using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace final_pro.Models
{
    public class Postbook
    {
        [Key]
        public string title { get; set; }
        public string author { get; set; }
        public int ISBN { get; set; }
        public int number { get; set; }
        public int year { get; set; }
    }
}
