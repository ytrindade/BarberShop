using BarberShop.Domain.Repositories;

namespace BarberShop.Infrastructure.DataAccess.Repositories;
internal class UnitOfWork : IUnitOfWork
{
    private readonly BarberShopDbContext _dbContext;

    public UnitOfWork(BarberShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
