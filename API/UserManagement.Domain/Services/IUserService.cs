using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Models;

namespace UserManagement.Domain.Services
{
    public interface IUserService
    {
        Task<string> AuthenticateAsync(string login, string password);
        Task<IReadOnlyCollection<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> CreateAsync(User user, string password);
        Task<User> UpdateAsync(User user, string password = null);
        Task<User> DeleteAsync(Guid id);
    }
}
