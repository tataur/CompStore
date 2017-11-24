using System.Collections.Generic;
using CompStore.Domain.Entities;
using CompStore.Domain.Abstract;

namespace CompStore.Domain.Concrete
{
    public class EFCompRepository : ICommonRepository<Comp>
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Comp> Items
        {
            get { return context.Computers; }
        }

        public void SaveChanges(Comp comp)
        {
            if (comp.CompId == null)
            {
                context.Computers.Add(comp);
            }
            else
            {
                Comp dbEntry = context.Computers.Find(comp.CompId);
                if (dbEntry != null)
                {
                    dbEntry.Name = comp.Name;
                    dbEntry.Description = comp.Description;
                    dbEntry.Price = comp.Price;
                    dbEntry.Category = comp.Category;
                }
                context.SaveChanges();
            }
        }

        public Comp Delete(int compId)
        {
            Comp dbEntry = context.Computers.Find(compId);
            if (dbEntry != null)
            {
                context.Computers.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
