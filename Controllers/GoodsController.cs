using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GoodsController : ControllerBase
    {
        [HttpGet("getgoods")]
        public async Task<object> GetGoods([FromQuery] dynamic query)
        {

            return null;
        }
    }
}
