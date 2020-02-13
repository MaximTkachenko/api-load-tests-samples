using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _db.Get(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Num num)
        {
            await _db.Add(new Num { Id = Guid.NewGuid().ToString(), Name = num.Name });
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Num num)
        {
            await _db.Update(new Num { Id = id.ToString(), Name = num.Name });
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _db.Delete(id);
            return Ok();
        }
    }
}
