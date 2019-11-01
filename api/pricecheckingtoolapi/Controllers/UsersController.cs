using Microsoft.AspNetCore.Authorization;
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
        
        [HttpPost("auth/login")]
        public IActionResult Authenticate([FromForm] User userparam)
        {
            if (string.IsNullOrWhiteSpace(userparam.name) || string.IsNullOrWhiteSpace(userparam.sessionId))
                return BadRequest();

            var user = databaseContext.Users.SingleOrDefault(x => x.name == userparam.name);

            if (user == null)
                return NotFound();

            return Ok();
        }

        [HttpPost("auth/create")]
        public IActionResult Create([FromForm] User userparam)
        {            
            if (string.IsNullOrWhiteSpace(userparam.name) ||string.IsNullOrWhiteSpace(userparam.sessionId))
                return BadRequest();

            User user = new User();

            if (databaseContext.Users.Any(x => x.name == userparam.name))
                return null;

            user.name = userparam.name;
            user.sessionId = userparam.sessionId;

            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();

            return Ok(new User()
            {
                name = user.name,
                userId = user.userId
            });
        }
    }
}
