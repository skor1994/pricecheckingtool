using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Models
{
    public class Currency
    {
        [Key]
        [StringLength(35)]
        public string currencyTypeName { get; set; }
        public double chaosEquivalent { get; set; }
    }
}