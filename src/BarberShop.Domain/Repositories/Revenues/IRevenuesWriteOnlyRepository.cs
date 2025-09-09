using BarberShop.Domain.Entities;

namespace BarberShop.Domain.Repositories.Revenues;
public interface IRevenuesWriteOnlyRepository
{
    Task Add(Revenue revenue);
    Task<bool> Delete(long id);
}
