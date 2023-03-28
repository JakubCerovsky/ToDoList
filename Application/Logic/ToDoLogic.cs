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
    
    public Task<IEnumerable<ToDo>> GetAsync(SearchToDoParametersDTO searchParameters)
    {
        return todoDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(ToDoUpdateDTO dto)
    {
        ToDo? existing = await todoDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        if (dto.IsCompleted != null && existing.IsCompleted && !(bool)dto.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed Todo");
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        bool completedToUse = dto.IsCompleted ?? existing.IsCompleted;
    
        ToDo updated = new (userToUse, titleToUse)
        {
            IsCompleted = completedToUse,
            Id = existing.Id,
        };

        ValidateTodo(updated);

        await todoDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        ToDo? todo = await todoDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Todo with ID {id} was not found!");
        }

        if (!todo.IsCompleted)
        {
            throw new Exception("Cannot delete un-completed Todo!");
        }

        await todoDao.DeleteAsync(id);
    }
    
    public async Task<GetToDoDTO> GetByIdAsync(int id)
    {
        ToDo? todo = await todoDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Todo with id {id} not found");
        }

        return new GetToDoDTO(todo.Id, todo.Owner.UserName, todo.Title, todo.IsCompleted);
    }

    private void ValidateTodo(ToDo dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}