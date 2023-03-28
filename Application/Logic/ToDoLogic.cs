using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Models.DTOs;
using Models.Models;

namespace Application.Logic;

public class ToDoLogic : IToDoLogic
{
    private readonly IToDoDAO todoDao;
    private readonly IUserDAO userDao;

    public ToDoLogic(IToDoDAO todoDao, IUserDAO userDao)
    {
        this.todoDao = todoDao;
        this.userDao = userDao;
    }

    public async Task<ToDo> CreateAsync(ToDoCreationDTO dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        ValidateToDo(dto);
        ToDo todo = new ToDo(user, dto.Title);
        ToDo created = await todoDao.CreateAsync(todo);
        return created;
    }

    private void ValidateToDo(ToDoCreationDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}