using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Models
{
    public class User
    {
        [Required]
        public string name { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
        public List<PartyUser> partyUser { get; set; }

    }
}
