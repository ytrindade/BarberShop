using BarberShop.Communication.Requests;
using BarberShop.Exception;
using FluentValidation;

namespace BarberShop.Application.UseCases.Revenues;
public class RevenueValidator : AbstractValidator<RequestRevenueJson>
{
    public RevenueValidator()
    {
        RuleFor(r => r.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_IS_REQUIRED);
        RuleFor(r => r.Date).LessThanOrEqualTo(DateTime.Now).WithMessage(ResourceErrorMessages.REVENUES_FOR_PAST_DATES);
        RuleFor(r => r.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(r => r.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }
}
