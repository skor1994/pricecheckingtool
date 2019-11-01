using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Models
{
    public class Item 
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int links { get; set; }
        public string baseType { get; set; }
        public double chaosValue { get; set; }
        public double exaltedValue { get; set; }
        public int mapTier { get; set; }
    }
}
