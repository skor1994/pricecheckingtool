using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pricecheckingtoolapi.Db;
using pricecheckingtoolapi.Models;
using pricecheckingtoolapi.Dtos;
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
        public IActionResult Authenticate([FromForm] UserDto userparam)
        {
            if (string.IsNullOrWhiteSpace(userparam.name))
                return BadRequest();

            var user = databaseContext.Users.SingleOrDefault(x => x.name == userparam.name);

            if (user == null)
                return NotFound();

            if (!VerifyPasswordHash(userparam.password, user.passwordHash, user.passwordSalt))
                return null;

            return Ok(new User()
            {
                name = user.name,
                userId = user.userId
            });
        }

        [HttpPost("auth/create")]
        public IActionResult Create([FromForm] UserDto userparam)
        {            
            if (string.IsNullOrWhiteSpace(userparam.name) ||string.IsNullOrWhiteSpace(userparam.password))
                return BadRequest();

            User user = new User();

            if (databaseContext.Users.Any(x => x.name == userparam.name))
                return null;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userparam.password, out passwordHash, out passwordSalt);

            user.name = userparam.name;
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;

            databaseContext.Users.Add(user);
            databaseContext.SaveChanges();

            return Ok(new User()
            {
                name = user.name,
                userId = user.userId
            });
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
