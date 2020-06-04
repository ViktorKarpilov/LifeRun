using UserManagement.API.Requests;
using FluentValidation;

namespace UserManagement.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        private readonly int minPassLength = 8;
        private readonly int minLoginLength = 4;

        private const string notNull = "must not be empty";
        private const string notEmpty = "must not be empty or consist of whitespaces only";

        public RegisterRequestValidator()
        {
            RuleFor(user => user.Login).NotNull().WithMessage("Login " + notNull);
            RuleFor(user => user.Password).NotNull();
            RuleFor(user => user.Role).NotNull();
            RuleFor(user => user.Login).NotEmpty().WithMessage("Login " + notEmpty);
            RuleFor(user => user.Login.Length).GreaterThanOrEqualTo(minLoginLength).WithMessage("Your Login must be at least " + minLoginLength + " characters long");
            RuleFor(user => user.FirstName).NotNull();
            RuleFor(user => user.FirstName).NotEmpty();
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(minPassLength).WithMessage("Your password must be at least " + minPassLength + " characters long");
            RuleFor(user => user.LastName).NotNull();
            RuleFor(user => user.LastName).NotEmpty();
            //RuleFor(user => user.Email).EmailAddress();
        }
    }
}
