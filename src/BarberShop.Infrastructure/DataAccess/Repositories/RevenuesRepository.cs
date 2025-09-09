using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories.Revenues;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.DataAccess.Repositories;
internal class RevenuesRepository : IRevenuesWriteOnlyRepository, IRevenuesReadOnlyRepository, IRevenuesUpdateOnly
{
    private readonly BarberShopDbContext _dbContext;

    public RevenuesRepository(BarberShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Revenue revenue) => await _dbContext.Revenues.AddAsync(revenue);

    async Task<Revenue?> IRevenuesReadOnlyRepository.GetById(long id) 
        => await _dbContext.Revenues.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

    async Task<Revenue?> IRevenuesUpdateOnly.GetById(long id)
        => await _dbContext.Revenues.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<List<Revenue>> GetAll() => await _dbContext.Revenues.AsNoTracking().ToListAsync();

    public async Task<bool> Delete(long id)
    {
        var revenue = await _dbContext.Revenues.FirstOrDefaultAsync(r => r.Id == id);

        if(revenue is null) return false;

        _dbContext.Revenues.Remove(revenue);
        return true;

    }   

    public void Update(Revenue revenue) =>  _dbContext.Update(revenue);

    public async Task<List<Revenue>> FilterByMonth(DateOnly date)
    {
        var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1);
        var endtDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext.
            Revenues
            .AsNoTracking()
            .Where(revenue => revenue.Date >= startDate && revenue.Date <= endtDate)
            .OrderByDescending(revenue => revenue.Date)
            .ThenBy(revenue => revenue.Title)
            .ToListAsync();        
    }
}
