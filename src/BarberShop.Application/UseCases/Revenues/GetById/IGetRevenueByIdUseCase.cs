using BarberShop.Communication.Responses;

namespace BarberShop.Application.UseCases.Revenues.GetById;
public interface IGetRevenueByIdUseCase
{
    Task<ResponseRevenueJson> Execute(long id);
}
