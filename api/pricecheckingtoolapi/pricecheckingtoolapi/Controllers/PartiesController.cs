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

        [HttpGet("{id}")]
        public async Task<ActionResult<Party>> GetSingleParty(int id)
        {
            var party = await databaseContext.Partys.FindAsync(id);

            if (party == null)
            {
                return NotFound();
            }

            return party;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Party>>> GetAll()
        {
            return await databaseContext.Partys.ToListAsync(); ;
        }
    }
}
