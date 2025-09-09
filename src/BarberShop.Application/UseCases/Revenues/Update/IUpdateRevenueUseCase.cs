using BarberShop.Communication.Requests;

namespace BarberShop.Application.UseCases.Revenues.Update;
public interface IUpdateRevenueUseCase
{
    Task Execute(long id, RequestRevenueJson request);
}
