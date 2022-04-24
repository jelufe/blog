using Blog.Domain.Entities;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class SharingValidator : AbstractValidator<Sharing>
    {
        public SharingValidator()
        { }
    }
}
