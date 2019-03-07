using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Controller class to handle the todos.
    /// </summary>
    /// <remarks>
    /// This class contains GET, POST, PUT, DELETE methods for creating, reading, updating, and deleting the todo list.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private TodoContext _context;

        /// <summary>
        /// TodoController controls the todos being added to the list.
        /// </summary>
        public TodoController(TodoContext context)
        {
            _context = context;

            /*
                // Adding a todo for testing purposes...

                if (_context.TodoItems.Count() == 0)
                {
                    // This is a default todo for the list
                    _context.TodoItems.Add(new TodoItem { Name = "Create a functioning todo application", IsComplete = false });
                    _context.SaveChanges();
                }
            */
        }

        /// <summary>
        /// GET method for getting the list of todo items.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        /// <summary>
        /// GET method for getting the todo item via the todo ID.
        /// </summary>
        /// <remarks>
        /// The method takes in an id parameter and checks for an item.
        /// if there is no todos, it returns not found, otherwise it returns the item.
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        /// <summary>
        /// POST method to create a todo.
        /// </summary>
        /// <remarks>
        /// The method takes in a TodoItem and assigns an id to the item.
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        /// <summary>
        /// PUT method to edit the information of the todo item.
        /// </summary>
        /// <remarks>
        /// The method takes in an id and the todo item.
        /// it updates the state of the todo that matches the requested id.
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// DELETE method to remove an item from the list.
        /// </summary>
        /// <remarks>
        /// The method takes an id and checks for the todo.
        /// If the todo is found, it is removed from the list.
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
