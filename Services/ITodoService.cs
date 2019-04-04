using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public interface ITodoService
    {
        Task<IReadOnlyCollection<TodoModel>> Find();

        Task<TodoModel> FindOne(int id);

        Task<TodoModel> CreateTodo(TodoModel todoModel);

        Task<TodoModel> UpdateTodo(TodoModel todoModel); 

        Task DeleteTodo(int todoId);
    }
}
