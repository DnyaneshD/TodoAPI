using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public class TodoService : ITodoService
    {
        TodoContext _context;

        public TodoService(TodoContext context) => _context = context;

        public async Task<IEnumerable<TodoModel>> Find()
        {
            return await _context.Todos.AsNoTracking().ToListAsync();
        }

        public async Task<TodoModel> FindOne(int id)
        {
            return await _context.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TodoModel> CreateTodo(TodoModel todoModel)
        {
            await _context.Todos.AddAsync(todoModel);

            await _context.SaveChangesAsync();

            return await _context.Todos.SingleAsync(t => t.Id == todoModel.Id);
        }

        public async Task<TodoModel> UpdateTodo(TodoModel todoModel)
        {
            _context.Todos.Update(todoModel);

            await _context.SaveChangesAsync();

            return await _context.Todos.SingleAsync(t => t.Id == todoModel.Id);
        }

        public async Task DeleteTodo(int todoId)
        {
            var todoItem = await _context.Todos.FirstAsync((item) => item.Id == todoId);

            _context.Todos.Remove(todoItem);

            await _context.SaveChangesAsync();
        }
    }
}

