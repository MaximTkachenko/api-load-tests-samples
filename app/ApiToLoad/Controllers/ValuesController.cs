using System;
using ApiToLoad.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiToLoad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDb _db;

        public ValuesController(IDb db)
        {
            _db = db;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_db.Get(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Num num)
        {
            _db.Add(new Num { Id = Guid.NewGuid(), Name = num.Name });
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Num num)
        {
            _db.Update(new Num { Id = id, Name = num.Name });
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _db.Delete(id);
            return Ok();
        }
    }
}
