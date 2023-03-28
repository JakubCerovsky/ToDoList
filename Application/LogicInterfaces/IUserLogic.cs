using Models.DTOs;
using Models.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDTO userToCreate);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters);
}