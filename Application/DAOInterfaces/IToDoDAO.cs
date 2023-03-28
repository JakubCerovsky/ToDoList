using Models.DTOs;
using Models.Models;

namespace Application.DAOInterfaces;

public interface IToDoDAO
{
    Task<ToDo> CreateAsync(ToDo todo);
    Task<IEnumerable<ToDo>> GetAsync(SearchToDoParametersDTO searchParameters);
    Task UpdateAsync(ToDo todo);
    Task<ToDo?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}