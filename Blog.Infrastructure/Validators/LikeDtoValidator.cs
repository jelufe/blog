using Blog.Domain.DTOs;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class LikeDtoValidator : AbstractValidator<LikeDto>
    {
        public LikeDtoValidator()
        { }
    }
}
