using System.Data.Entity;
using CompStore.Domain.Entities;

namespace CompStore.DAL.Context
{
    public class EFDbContext : DbContext
    {
        public DbSet<Comp> Computers { get; set; }
        public DbSet<DeliveryDetails> DeliveryDetails { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EFDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}