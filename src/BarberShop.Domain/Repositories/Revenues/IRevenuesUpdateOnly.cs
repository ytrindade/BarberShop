using BarberShop.Domain.Entities;

public interface IRevenuesUpdateOnly
{
    Task<Revenue?> GetById(long id);
    void Update(Revenue revenue);
}
