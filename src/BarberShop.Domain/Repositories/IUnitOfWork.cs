namespace BarberShop.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
