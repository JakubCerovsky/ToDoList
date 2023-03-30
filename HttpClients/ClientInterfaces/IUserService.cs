using Models.DTOs;
using Models.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDTO dto);
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
}