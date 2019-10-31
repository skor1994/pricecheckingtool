using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pricecheckingtoolapi.Db;

namespace pricecheckingtoolapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public ItemsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
    }
}