using BarberShop.Application.UseCases.Revenues;
using BarberShop.Communication.Enums;
using BarberShop.Exception;
using CommonTestsUtilities.Requests;
using Shouldly;

namespace Validators.test.Revenues.Register;
public class RegisterRevenueValidator
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();

        //Act
        var result  = validator.Validate(request);

        //Assert
        result.IsValid.ShouldBeTrue();
    }


    [Fact]
    public void Error_Title_Empty()
    {
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Title = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            errors => errors.Count.ShouldBe(1),
            errors => errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_IS_REQUIRED))
            );
    }

    [Fact]
    public void Error_Future_Dates()
    {
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Date = DateTime.Now.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            errors => errors.Count.ShouldBe(1),
            errors => errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.REVENUES_FOR_PAST_DATES))
            );
    }

    [Fact]
    public void Error_Payment_Invalid()
    {
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.PaymentType = (PaymentType)4;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            errors => errors.Count.ShouldBe(1),
            errors => errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID))
            );
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Amount_Invalid(decimal amount)
    {
        var validator = new RevenueValidator();
        var request = RequestRegisterRevenueJsonBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(
            errors => errors.Count.ShouldBe(1),
            errors => errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO))
            );
    }
}
