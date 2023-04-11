using Automaton.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Automaton.DataAccesLayer.Context
{
    public interface IAutomatonDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Summary> Summaries { get; set; }
        int Save();
    }
}
