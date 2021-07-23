using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<string> Get()// return all
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)// return specific
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string categoryName)// POST aka Create
        {
        }

        [HttpPut]// PUT aka Update
        public void Put(string categoryName, [FromBody] string newCategoryName)
        {
        }

        [HttpDelete]
        public void Delete(string categoryName)
        {
        }
    }
}
