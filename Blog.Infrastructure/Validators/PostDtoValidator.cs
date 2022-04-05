using Blog.Domain.DTOs;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class PostDtoValidator : AbstractValidator<PostDto>
    {
        public PostDtoValidator()
        {
            RuleFor(user => user.Title)
                .NotNull()
                .Length(1, 200);

            RuleFor(user => user.Description)
                .NotNull()
                .Length(1, 8000);
        }
    }
}
