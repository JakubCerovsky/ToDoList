using Application.DAOInterfaces;
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
}