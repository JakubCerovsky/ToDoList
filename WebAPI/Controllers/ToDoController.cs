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
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDo>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] bool? completedStatus, [FromQuery] string? titleContains)
    {
        try
        {
            SearchToDoParametersDTO parameters = new(userName, userId, completedStatus, titleContains);
            IEnumerable<ToDo> toDos = await toDoLogic.GetAsync(parameters);
            return Ok(toDos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] ToDoUpdateDTO dto)
    {
        try
        {
            await toDoLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await toDoLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetToDoDTO>> GetById([FromRoute] int id)
    {
        try
        {
            GetToDoDTO result = await toDoLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}