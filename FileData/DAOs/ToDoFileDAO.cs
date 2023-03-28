using Application.DAOInterfaces;
using Models.DTOs;
using Models.Models;

namespace FileData.DAOs;

public class ToDoFileDAO:IToDoDAO
{
    private readonly FileContext context;

    public ToDoFileDAO(FileContext context)
    {
        this.context = context;
    }

    public Task<ToDo> CreateAsync(ToDo todo)
    {

        int id = 1;
        if (context.ToDos.Any())
        {
            id = context.ToDos.Max(td => td.Id);
            id++;
        }

        todo.Id = id;

        context.ToDos.Add(todo);
        context.SaveChanges();

        return Task.FromResult(todo);
    }
    
    public Task<IEnumerable<ToDo>> GetAsync(SearchToDoParametersDTO searchParameters)
    {
        IEnumerable<ToDo> toDo = context.ToDos.AsEnumerable();
        
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            toDo = context.ToDos.Where(td => td.Owner.UserName.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }
        
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            toDo = context.ToDos.Where(td => td.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }
        
        if (searchParameters.UserId != null)
        {
            toDo = context.ToDos.Where(td => td.Owner.Id==searchParameters.UserId);
        }
        
        if (searchParameters.CompletedStatus != null)
        {
            toDo = context.ToDos.Where(td => td.IsCompleted==searchParameters.CompletedStatus);
        }
        
        

        return Task.FromResult(toDo);
    }

    public Task UpdateAsync(ToDo todo)
    {
        ToDo? existing = context.ToDos.FirstOrDefault(td => td.Id == todo.Id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {todo.Id} does not exist!");
        }

        context.ToDos.Remove(existing);
        context.ToDos.Add(todo);
    
        context.SaveChanges();
    
        return Task.CompletedTask;
    }

    public Task<ToDo?> GetByIdAsync(int id)
    {
        ToDo? existing = context.ToDos.FirstOrDefault(td =>
            td.Id==id
        );
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        ToDo? todo = context.ToDos.FirstOrDefault(td =>
            td.Id==id
        );
        if (todo == null)
        {
            throw new Exception($"Todo with ID {id} was not found!");
        }
        context.ToDos.Remove(todo);
    
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
}