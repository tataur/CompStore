using CompStore.Domain.Entities;
using System.Data.Entity;

namespace CompStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Comp> Computers { get; set; }
    }
}
