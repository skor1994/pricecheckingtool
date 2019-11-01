using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricecheckingtoolapi.Db;
using pricecheckingtoolapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartiesController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public PartiesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet("getmyparties/{username}")]
        public IQueryable<Party> GetAllPartiesById(string username)
        {
            var myParties = (
                            from partyUser in databaseContext.PartyUser
                            join party in databaseContext.Partys on partyUser.partyId equals party.partyId
                            where partyUser.username == username
                            select new Party
                            {
                                name = party.name,
                                partyId = party.partyId
                            });

            return myParties;
        }

        [HttpGet("getuserfrom/{partyId}")]
        public IQueryable<User> GetAllUserById(int partyId)
        {
            var containeduser = (
                            from party in databaseContext.PartyUser
                            join user in databaseContext.Users on party.username equals user.username
                            where party.partyId == partyId
                            select new User
                            {
                                username = user.username
                            });

            return containeduser;
        }
    }
}
