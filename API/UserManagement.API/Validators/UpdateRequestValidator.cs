using FluentValidation;
using UserManagement.API.Requests;
using UserManagement.Domain.Models;
using UserManagement.API.Validators;
namespace UserManagement.API.Validators
{
    public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
    {
        public UpdateRequestValidator()
        {
            RuleFor(user => new RegisterRequest()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                Role = Role.Worker
            }).SetValidator(new RegisterRequestValidator());
        }
    }
}
