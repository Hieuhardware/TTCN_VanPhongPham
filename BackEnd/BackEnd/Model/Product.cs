using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class Product
    {
        public int id { get; set; }
        public int cat_id { get; set; }
        public string? name { get; set; }
        public string? origin { get; set; }
        public int price { get; set; }
        public string? image { get; set; }
        public string? description { get; set; }
        public int amount { get; set; }
        public string? unit { get; set; }
    }
}
