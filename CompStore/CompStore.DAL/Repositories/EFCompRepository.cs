using System.Collections.Generic;
using CompStore.Domain.Entities;
using CompStore.Domain.Interfaces;
using CompStore.DAL.Context;

namespace CompStore.DAL.Repositories {
    public class EFCompRepository : ICommonRepository<Comp>
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IEnumerable<Comp> AllItems
        {
            get { return _context.Computers; }
        }

        public void SaveChanges(Comp comp)
        {
            if (comp.Id == null)
            {
                _context.Computers.Add(comp);
            }
            else
            {
                Comp dbEntry = _context.Computers.Find(comp.Id);
                if (dbEntry != null)
                {
                    dbEntry.FillCommonFields();
                    dbEntry.Name = comp.Name;
                    dbEntry.Description = comp.Description;
                    dbEntry.Price = comp.Price;
                    dbEntry.Category = comp.Category;
                    dbEntry.Quantity = comp.Quantity;
                }

                _context.SaveChanges();
            }
        }

        public Comp DeleteItem(int compId)
        {
            Comp dbEntry = _context.Computers.Find(compId);
            if (dbEntry != null)
            {
                _context.Computers.Remove(dbEntry);
                _context.SaveChanges();
            }

            return dbEntry;
        }
    }
}