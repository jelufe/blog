using Blog.Domain.DTOs;
using FluentValidation;

namespace Blog.Infrastructure.Validators
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
