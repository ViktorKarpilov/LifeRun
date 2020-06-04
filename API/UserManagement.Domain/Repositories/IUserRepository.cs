using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Models;

namespace UserManagement.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> UpdateAsync(User userModel);
        Task<User> CreateAsync(User userModel);
        Task<User> DeleteAsync(Guid userId);
        Task<IReadOnlyCollection<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid userId);
        Task<User> GetByLoginAsync(string userLogin);
    }
}
