using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoAPI.Models;
using TodoAPI.Services;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _todoService.Find();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _todoService.FindOne(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TodoModel model)
        {
            if (model.Details == null)
            {
                return StatusCode(422);
            }

            var result = await _todoService.CreateTodo(model);
            return Created(result.Id.ToString(), result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TodoModel model)
        {
            if (id < 0 || model.Details == null)
            {
                return StatusCode(422);
            }

            var result = await _todoService.UpdateTodo(model);
            return Accepted(result.Id.ToString(), result);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return StatusCode(422);
            }

            await _todoService.DeleteTodo(id);
            return NoContent();
        }
    }
}
