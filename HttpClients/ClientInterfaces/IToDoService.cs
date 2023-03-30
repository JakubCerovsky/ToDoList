using Models.DTOs;
using Models.Models;

namespace HttpClients.ClientInterfaces;

public interface IToDoService
{
    Task CreateAsync(ToDoCreationDTO dto);
    Task<ICollection<ToDo>> GetAsync(
        string? userName, 
        int? userId, 
        bool? completedStatus, 
        string? titleContains
    );
}