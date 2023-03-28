using Models.DTOs;
using Models.Models;

namespace Application.DAOInterfaces;

public interface IUserDAO
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchParameters);
    Task<User?> GetByIdAsync(int dtoOwnerId);
}