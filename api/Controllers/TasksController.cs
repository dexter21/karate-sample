using System.Collections.Generic;
using System.Linq;
using api.Auth;
using api.Items;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(AuthAttribute))]
    public class TasksController : ControllerBase
    {
        private static readonly IDictionary<int, Item> _tasks = new Dictionary<int, Item>();

        [HttpGet]
        public IEnumerable<Item> Get() => _tasks.Values;

        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
            => _tasks.TryGetValue(id, out Item item) ? item : NotFound();

        [HttpPost]
        public IActionResult Add([FromBody] Item model)
        {
            model.Id = _tasks.Select(x => x.Key).DefaultIfEmpty().Max() + 1;
            _tasks.Add(model.Id, model);
            return Created(string.Empty, model.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Item model)
        {
            if (!_tasks.ContainsKey(id))
            {
                return NotFound();
            }

            _tasks[id] = model;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tasks.Remove(id);
            return NoContent();
        }
        
        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _tasks.Clear();
            return NoContent();
        }
    }
}