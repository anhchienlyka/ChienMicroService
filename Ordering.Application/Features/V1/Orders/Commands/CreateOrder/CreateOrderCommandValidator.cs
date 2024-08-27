using FluentValidation;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("{Username} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Username} must not exceed 50 characters.");
        }
    }
}