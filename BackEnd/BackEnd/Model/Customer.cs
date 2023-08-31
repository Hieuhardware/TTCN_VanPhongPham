using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Model
{
    public class Customer
    {
        public int id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public string? tel { get; set; }
        public string? address { get; set; }
        public DateTime date_create { get; set; }
    }
}
