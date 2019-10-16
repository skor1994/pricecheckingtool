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

        [HttpPost("join/{userId},{partyId}")]
        public async Task<IActionResult> JoinParty(int userId, int partyId)
        {
            PartyUser partyUser = new PartyUser();
            partyUser.userId = userId;
            partyUser.partyId = partyId;

            databaseContext.PartyUser.Add(partyUser);
            await databaseContext.SaveChangesAsync();

            return Ok($"Successfully joined party with ID: {partyUser.party.partyId}");
        }

        [HttpPost("create/{userId},{name}")]
        public async Task<IActionResult> CreateParty(int userId, string name)
        {
            PartyUser partyUser = new PartyUser();
            partyUser.userId = userId;
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
