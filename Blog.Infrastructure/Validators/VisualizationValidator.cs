using Blog.Domain.Entities;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class VisualizationValidator : AbstractValidator<Visualization>
    {
        public VisualizationValidator()
        { }
    }
}
