using Blog.Domain.Entities;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class LikeValidator : AbstractValidator<Like>
    {
        public LikeValidator()
        { }
    }
}
