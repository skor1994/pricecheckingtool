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
    public class UsersController: ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public UsersController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet("{id}")]
        public async Task<User> GetSingleUser(int id)
        {
            var user = await databaseContext.Users.FindAsync(id);

            if (user == null)
            {
                //return NotFound();
            }

            return user;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await databaseContext.Users.ToListAsync(); ;
        }

        [HttpPost("{name}")]
        public async Task<ActionResult<User>> AddUser(string name)
        {
            User user = new User();
            user.name = name;
            databaseContext.Users.Add(user);
            await databaseContext.SaveChangesAsync();

            return user;
        }
    }
}
