using BarberShop.Domain.Entities;

namespace BarberShop.Domain.Repositories.Revenues;
public interface IRevenuesReadOnlyRepository
{
    Task<List<Revenue>> GetAll();
    Task<Revenue?> GetById(long id);
    Task<List<Revenue>> FilterByMonth(DateOnly date);
}
