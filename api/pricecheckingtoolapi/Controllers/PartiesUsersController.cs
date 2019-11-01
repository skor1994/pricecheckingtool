using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
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
    public class PartiesUsersController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public PartiesUsersController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost("join/{username},{partyId}")]
        public async Task<IActionResult> JoinParty(string username, int partyId)
        {
            PartyUser partyUser = new PartyUser();
            partyUser.username = username;
            partyUser.partyId = partyId;

            databaseContext.PartyUser.Add(partyUser);
            await databaseContext.SaveChangesAsync();

            return Ok($"Successfully joined party with ID: {partyUser.party.partyId}");
        }

        [HttpPost("create/{username},{name}")]
        public async Task<IActionResult> CreateParty(string username, string name)
        {
            PartyUser partyUser = new PartyUser();
            partyUser.username = username;
            partyUser.party = new Party()
            {
                name = name
            };
            
            databaseContext.PartyUser.Add(partyUser);
            await databaseContext.SaveChangesAsync();
            
            return Ok($"Successfully created party with name: {partyUser.party.name}");
        }
    }
}
