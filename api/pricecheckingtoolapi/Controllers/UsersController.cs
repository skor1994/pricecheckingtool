using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricecheckingtoolapi.Db;
using pricecheckingtoolapi.Models;
using pricecheckingtoolapi.Providers;
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
        private readonly PricesProvider _pricesProvider; 
        public UsersController(DatabaseContext databaseContext, PricesProvider pricesProvider)
        {
            this.databaseContext = databaseContext;
            _pricesProvider = pricesProvider;
        }
        
        [HttpPost("auth/login")]
        public IActionResult Authenticate([FromForm] User userparam)
        {
            if (string.IsNullOrWhiteSpace(userparam.username) || string.IsNullOrWhiteSpace(userparam.sessionId))
                return BadRequest();

            var user = databaseContext.Users.Find(userparam.username);

            if (user == null)
                return NotFound();

            return Ok();
        }

        [HttpPost("auth/create")]
        public IActionResult Create([FromForm] User userparam)
        {            
            if (string.IsNullOrWhiteSpace(userparam.username) ||string.IsNullOrWhiteSpace(userparam.sessionId))
                return BadRequest();

            User user = new User();

            if (databaseContext.Users.Any(x => x.username == userparam.username))
                return null;

            user.username = userparam.username;
            user.sessionId = userparam.sessionId;

            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();

            return Ok();
        }

        [HttpGet("getstashtabs/{username}")]
        public async Task<IActionResult> GetItems(string username)
        {
            var user = new User();

            if (string.IsNullOrWhiteSpace(username))
                return BadRequest();
            
            user = databaseContext.Users.Find(username);

            if (user == null)
                return NotFound();
            
            return Ok(await _pricesProvider.FetchStashTabsData(user));
        }
    }
}
