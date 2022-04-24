using Blog.Domain.DTOs;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class SharingDtoValidator : AbstractValidator<SharingDto>
    {
        public SharingDtoValidator()
        { }
    }
}
