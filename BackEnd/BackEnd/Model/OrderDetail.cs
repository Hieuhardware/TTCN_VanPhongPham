using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class OrderDetail
    {
        public int prod_id { get; set; }
        public int order_id { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int cat_id { get; set; }
        public string? origin { get; set; }
        public string? image { get; set; }
        public string? description { get; set; }
        public int amount { get; set; }
        public string? unit { get; set; }
        public int cus_id { get; set; }
        public DateTime order_date { get; set; }
        public string? status { get; set; }
        public string? notes { get; set; }
        public string? address { get; set; }
        public string? tel { get; set; }
        public string? name { get; set; }
    }
}
