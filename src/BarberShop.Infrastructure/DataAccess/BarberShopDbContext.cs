using BarberShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Infrastructure.DataAccess;
internal class BarberShopDbContext : DbContext
{
    public BarberShopDbContext(DbContextOptions options) : base(options) {  }
    public DbSet<Revenue> Revenues { get; set; }
}
