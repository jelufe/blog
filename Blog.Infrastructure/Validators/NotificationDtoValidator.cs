using Blog.Domain.DTOs;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class NotificationDtoValidator : AbstractValidator<NotificationDto>
    {
        public NotificationDtoValidator()
        {
            RuleFor(notificationDto => notificationDto.Title)
                .NotNull()
                .Length(1, 200);

            RuleFor(notificationDto => notificationDto.Message)
                .NotNull()
                .Length(1, 8000);
        }
    }
}
