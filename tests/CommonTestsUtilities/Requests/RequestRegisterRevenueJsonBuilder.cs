using BarberShop.Communication.Enums;
using BarberShop.Communication.Requests;
using Bogus;

namespace CommonTestsUtilities.Requests;
public class RequestRegisterRevenueJsonBuilder
{
    public static RequestRevenueJson Build()
    {
        return new Faker<RequestRevenueJson>()
            .RuleFor(r => r.Title, f => f.Commerce.ProductName())
            .RuleFor(r => r.Description, f => f.Commerce.ProductDescription())
            .RuleFor(r => r.Date, f => f.Date.Past())
            .RuleFor(r => r.Amount, f => f.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.PaymentType, f => f.PickRandom<PaymentType>());        
    }
}
