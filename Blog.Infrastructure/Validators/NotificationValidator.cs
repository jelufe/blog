using Blog.Domain.Entities;
using FluentValidation;

namespace Blog.Infrastructure.Validators
{
    public class NotificationValidator : AbstractValidator<Notification>
    {
        public NotificationValidator()
        {
            RuleFor(notification => notification.NotificationId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(post => post.Title)
                .NotNull()
                .Length(1, 200);

            RuleFor(post => post.Message)
                .NotNull()
                .Length(1, 8000);
        }
    }
}
