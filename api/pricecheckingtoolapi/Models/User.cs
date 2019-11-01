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
        [Key]
        [StringLength(35)]
        public string username { get; set; }
        public string sessionId { get; set; }
        public string league { get; set; }
        public List<PartyUser> partyUser { get; set; }
        
    }
}
