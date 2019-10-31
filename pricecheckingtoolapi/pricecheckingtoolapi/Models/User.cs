﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Models
{
    public class User
    {
        public string name { get; set; }
        [Key]
        public int userId { get; set; }
        public string sessionId { get; set; }
        public List<PartyUser> partyUser { get; set; }
        public List<Item> items { get; set; }
        
    }
}
