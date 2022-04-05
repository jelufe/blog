using Blog.Domain.DTOs;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class PasswordDtoValidator : AbstractValidator<PasswordDto>
    {
        public PasswordDtoValidator()
        {
            RuleFor(user => user.Email)
                .NotNull()
                .Length(1, 200);

            RuleFor(user => user.OldPassword)
                .NotNull()
                .Length(1, 200);

            RuleFor(user => user.NewPassword)
                .NotNull()
                .Length(1, 200);
        }
    }
}
