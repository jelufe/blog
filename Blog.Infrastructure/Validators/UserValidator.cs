using Blog.Domain.Entities;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(user => user.Name)
                .NotNull()
                .Length(1, 200);

            RuleFor(user => user.Email)
                .NotNull()
                .Length(1, 200);
        }
    }
}
