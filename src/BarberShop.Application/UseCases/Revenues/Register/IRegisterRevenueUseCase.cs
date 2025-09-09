using BarberShop.Communication.Requests;
using BarberShop.Communication.Responses;

namespace BarberShop.Application.UseCases.Revenues.Register;
public interface IRegisterRevenueUseCase
{
    Task<ResponseRegisteredRevenueJson> Execute(RequestRevenueJson revenue);
}
