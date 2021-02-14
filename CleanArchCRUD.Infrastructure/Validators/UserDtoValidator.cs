using CleanArchCRUD.Domain.DTOs;
using FluentValidation;

namespace CleanArchCRUD.Infrastructure.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Name)
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
