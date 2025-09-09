using BarberShop.Communication.Responses;

namespace BarberShop.Application.UseCases.Revenues.GetAll;
public interface IGetRevenuesUseCase
{
    Task<ResponseRevenuesJson> Execute();
}
