using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace DatingApp.API.Controllers
{
    // route you need to access a particular controller. [controller] is a placeholder
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // private vars should have _ before name
        private readonly DataContext _context;

        // (CMD + .) bring in Context from Persistence
        public ValuesController(DataContext context)
        {
            // this._context = context;
            // this not necessary
            _context = context;
        }

        // GET api/values <- root of controller 
        // bring in Domain from Value class (CMD + .)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> Get()
        {
            // return all values from list as db. 
            // call on database from different thread so to not slow down other requests from db
            // ie were not blocking requests the threads come in on 
            var values = await _context.Values.ToListAsync(); // await return from db
            // return 200 OK response from API and pass values inside 
            return Ok(values);
            // but recommended approach is to make queries from db using asyncronous (more scalable)

            // so if you write a method that has potential to be long running (and any call to a db has that potential)
            // then its the recommended approach to use asyncronous approach.
        }

        // GET api/values/5. id is a root parameter 
        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> Get(int id)
        {
            // return only an element of a sequence that matches a condition 
            // return value if it finds it or default value (null)
            var value = await _context.Values.FindAsync(id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
