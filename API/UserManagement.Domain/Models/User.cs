using System;
using System.Dynamic;

namespace UserManagement.Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; private set; }

        public byte[] PasswordSalt { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid CompanyId { get; set; }

        // User is not supposed to change role on an existing account
        // If they want to switch role, they must create new account
        public string Role { get; private set; }

        // for the convenience of testing
        public User()
        {
        }

        public User(string login, string email, string firstName,
                    string lastName,  Guid companyId, string role, 
                    byte[] passwordHash, byte[] passwordSalt)
        {
            Id = new Guid(email);
            Login = login;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            CompanyId = companyId;
            Role = role;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetPasswordItems(byte[] passwordHash, byte[] passwordSalt)
        {

        }
    }

}