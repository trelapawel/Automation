using Automaton.Domain.Mappings;
using Automaton.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Automaton.DataAccesLayer.Context
{
    public class AutomatonDbContext : DbContext, IAutomatonDbContext
    {
        public AutomatonDbContext(
            DbContextOptions<AutomatonDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Summary> Summaries { get; set; }

        public int Save()
        {
            return SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new PaymentMap());
            modelBuilder.ApplyConfiguration(new SummaryMap());
        }
    }
}
