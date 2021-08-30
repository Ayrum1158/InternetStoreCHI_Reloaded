using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtFeaturedController : ControllerBase
    {
        protected int GetUserId()
        {
            return int.Parse(User.FindFirst("id").Value);
        }
    }
}
