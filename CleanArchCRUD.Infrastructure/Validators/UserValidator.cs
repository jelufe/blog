using CleanArchCRUD.Domain.DTOs;
using FluentValidation;

namespace CleanArchCRUD.Infrastructure.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Nome)
                .NotNull()
                .Length(1, 200);

            RuleFor(user => user.Email)
                .NotNull()
                .Length(1, 200);

            RuleFor(user => user.Password)
                .NotNull()
                .Length(1, 200);
        }
    }
}
