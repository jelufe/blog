using CleanArchCRUD.Domain.DTOs;
using FluentValidation;

namespace CleanArchCRUD.Infrastructure.Validators
{
    public class AuthDtoValidator : AbstractValidator<AuthDto>
    {
        public AuthDtoValidator()
        {
            RuleFor(auth => auth.Email)
                .NotNull()
                .Length(1, 200);

            RuleFor(auth => auth.Password)
                .NotNull()
                .Length(1, 200);
        }
    }
}
