using Blog.Domain.DTOs;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class VisualizationDtoValidator : AbstractValidator<VisualizationDto>
    {
        public VisualizationDtoValidator()
        { }
    }
}
