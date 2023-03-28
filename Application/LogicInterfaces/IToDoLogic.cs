using Models.DTOs;
using Models.Models;

namespace Application.LogicInterfaces;

public interface IToDoLogic
{
    Task<ToDo> CreateAsync(ToDoCreationDTO dto);
}