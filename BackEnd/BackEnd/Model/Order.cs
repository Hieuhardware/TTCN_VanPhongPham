using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class Order
    {
        public int id { get; set; }
        public int cus_id { get; set; }
        public string? order_date { get; set; }
        public string? status { get; set; }
        public string? notes { get; set; }
        public string? address { get; set; }
        public string? tel { get; set; }
        public string? name { get; set; }
    }
}
