using System;
using System.Collections.Generic;
using UserManagement.Domain.Services;
using UserManagement.Application.Helpers;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
using UserManagement.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace UserManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationSettings _authSettings;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IOptions<AuthenticationSettings> authSettings)
        {
            _authSettings = authSettings.Value;
            _userRepository = userRepository;
        }
        
        public async Task<string> AuthenticateAsync(string login, string password)
        {            
            var user = await _userRepository.GetByLoginAsync(login);

            string token;

            if (user == null)
            {
                return null;
            }
            try
            {
                if (!Cryptography.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }
                // authentication successful so generate jwt and refresh tokens
                token = JWTTokenProvider.GenerateAccessToken(_authSettings, user);
            }
            catch (ArgumentNullException anex)
            {
                throw new UserDomainException(anex.Message);
            }
            catch (ArgumentException aex)
            {
                throw new UserDomainException(aex.Message);
            }
            return token;
        }

        public Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            try
            {
                return _userRepository.GetAllAsync();
            }
            catch (InvalidOperationException ioex)
            {
                throw new UserDomainException(ioex.Message);
            }   
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<User> CreateAsync(User user, string password)
        {
            Task<User> userByLogin = _userRepository.GetByLoginAsync(user.Login);
            if (userByLogin.Result != null)
            {
                throw new UserDomainException("Login " + user.Login + " is already taken");
            }
                   
            byte[] passwordHash, passwordSalt;

            try
            {
                Cryptography.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.SetPasswordItems(passwordHash, passwordSalt);
            }
            catch(ArgumentNullException)
            {
                throw;
            }
            return  _userRepository.CreateAsync(user);
        }

        public Task<User> UpdateAsync(User userParam, string password = null)
        {
            var user = _userRepository.GetByIdAsync(userParam.Id);
            if (user.Result == null)
                throw new UserDomainException("User not found");
            
            // update login if it has changed
            if (userParam.Login != user.Result.Login)
            {
                // throw error if the new login is already taken
                if(_userRepository.GetByLoginAsync(userParam.Login).Result != null)
                    throw new UserDomainException("Login " + userParam.Login + " is already taken");
                user.Result.Login = userParam.Login;
            }

            user.Result.FirstName = userParam.FirstName;

            user.Result.LastName = userParam.LastName;

            user.Result.Email = userParam.Email;

            byte[] passwordHash, passwordSalt;
            try
            {
                Cryptography.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                
                user.Result.SetPasswordItems(passwordHash, passwordSalt);

                _userRepository.UpdateAsync(user.Result);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (UserDomainException)
            {
                throw;
            }

            return user;
        }

        public Task<User> DeleteAsync(Guid Id)
        {
            try
            {
                return _userRepository.DeleteAsync(Id);
            }
            catch (UserDomainException)
            {
                throw;
            }
        }
    }
}
