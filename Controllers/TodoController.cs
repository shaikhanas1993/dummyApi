using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dummyApi.Models;


namespace dummyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            

             _context = context;

            if(_context.TodoItems.Count() == 0)
            { 
                Console.WriteLine("request.............................");

                 _context.TodoItems.Add(new TodoItem { Name ="item1" });
                 _context.SaveChanges();
            }

        }
        
        
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Get()
        {
            var todoList = await _context.TodoItems.ToListAsync();
            
            return todoList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Get(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if(item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get),new { id = item.Id },item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public  async Task<IActionResult> Put(long id, TodoItem item)
        {
            if(id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State =  EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);    
           
            if(todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
