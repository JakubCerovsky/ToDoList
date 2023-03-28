using Models.DTOs;
using Models.Models;

namespace Application.LogicInterfaces;

public interface IToDoLogic
{
    Task<ToDo> CreateAsync(ToDoCreationDTO dto);
    Task<IEnumerable<ToDo>> GetAsync(SearchToDoParametersDTO searchParameters);
    Task UpdateAsync(ToDoUpdateDTO todo);
    Task DeleteAsync(int id);
    Task<GetToDoDTO> GetByIdAsync(int id);
}