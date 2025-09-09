using BarberShop.Domain.Entities;

namespace BarberShop.Application.UseCases.Revenues.Delete;
public interface IDeleteRevenueUseCase
{
    Task Execute(long id);
}
