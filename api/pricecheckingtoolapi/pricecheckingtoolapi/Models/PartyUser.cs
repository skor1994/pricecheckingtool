using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Models
{
    public class PartyUser
    {
        public int partyId { get; set; }
        public Party party { get; set; }

        public int userId { get; set; }
        public User user { get; set; }
    }
}
