using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Helpers;
using UserManagement.Domain.Models;
using UserManagement.Domain.Repositories;
using UserManagement.Persistence.Contexts;

namespace UserManagement.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementContext _userContext;
        private const string idNotFoundMessage = "User with requested id not found";

        public UserRepository(UserManagementContext userContext)
        {
            _userContext = userContext;
            _userContext.Database.EnsureCreated();
        }

        public async Task<User> CreateAsync(User userModel)
        {
            _userContext.Users.Add(userModel);
            await _userContext.SaveChangesAsync();
            return userModel;
        }

        public async Task<User> DeleteAsync(Guid userId)
        {
            try
            {
                var userToRemove = await _userContext.Users.SingleOrDefaultAsync(user => user.Id == userId);
                _userContext.Users.Remove(userToRemove);
                await _userContext.SaveChangesAsync();
                return userToRemove;
            }
            catch (InvalidOperationException)
            {
                throw new UserDomainException(idNotFoundMessage);
            }
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            try
            {
                return await _userContext.Users.ToListAsync();
            }
            catch (InvalidOperationException ioex)
            {
                throw new UserDomainException(ioex.Message);
            }
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            try
            {
                return await _userContext.Users.SingleOrDefaultAsync(user => user.Id == userId);
            }
            catch (InvalidOperationException)
            {
                throw new UserDomainException(idNotFoundMessage);
            }
        }

        public async Task<User> UpdateAsync(User userModel)
        {
            try
            {
                _userContext.Users.Update(userModel);
                await _userContext.SaveChangesAsync();
                return userModel;
            }
            catch (InvalidOperationException ioex)
            {
                throw new UserDomainException(ioex.Message);
            }
        }

        public async Task<User> GetByLoginAsync(string userLogin)
        {
            return await _userContext.Users.SingleOrDefaultAsync(user => user.Login == userLogin);
        }
    }
}
