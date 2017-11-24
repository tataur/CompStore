using CompStore.Domain.Entities;
using System.Data.Entity;

namespace CompStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Comp> Computers { get; set; }
        public DbSet<DeliveryDetails> DeliveryDetails { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EFDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
