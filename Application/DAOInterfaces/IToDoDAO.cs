using Models.Models;

namespace Application.DAOInterfaces;

public interface IToDoDAO
{
    Task<ToDo> CreateAsync(ToDo todo);
}