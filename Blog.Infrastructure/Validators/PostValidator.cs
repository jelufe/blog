using Blog.Domain.Entities;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(post => post.PostId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(post => post.Title)
                .NotNull()
                .Length(1, 200);

            RuleFor(post => post.Description)
                .NotNull()
                .Length(1, 8000);
        }
    }
}
