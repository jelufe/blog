using Blog.Domain.Entities;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(comment => comment.CommentId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(comment => comment.Message)
                .NotNull()
                .Length(1, 8000);
        }
    }
}
