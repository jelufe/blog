using Blog.Domain.Entities;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class ImageValidator : AbstractValidator<Image>
    {
        public ImageValidator()
        {
            RuleFor(image => image.ImageId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(image => image.Name)
                .NotNull()
                .Length(1, 200);

            RuleFor(image => image.Path)
                .NotNull()
                .Length(1, 200);
        }
    }
}
