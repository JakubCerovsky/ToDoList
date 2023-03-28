using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDosController : ControllerBase
{
    private readonly IToDoLogic toDoLogic;

    public ToDosController(IToDoLogic toDoLogic)
    {
        this.toDoLogic = toDoLogic;
    }

    [HttpPost]
    public async Task<ActionResult<ToDo>> CreateAsync([FromBody]ToDoCreationDTO dto)
    {
        try
        {
            ToDo toDo = await toDoLogic.CreateAsync(dto);
            return Created($"/todos/{toDo.Id}", toDo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}