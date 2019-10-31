using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Models
{
    public class Item
    {
        public string currencyTypeName { get; set; }
        public double chaosEquivalent { get; set; }
        [Key]
        public int id { get; set; }
    }
}
