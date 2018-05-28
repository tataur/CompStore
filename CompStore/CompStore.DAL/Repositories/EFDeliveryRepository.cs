using System.Collections.Generic;
using CompStore.DAL.Context;
using CompStore.Domain.Entities;
using CompStore.Domain.Interfaces;

namespace CompStore.DAL.Repositories
{
    public class EFDeliveryRepository : ICommonRepository<DeliveryDetails>
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IEnumerable<DeliveryDetails> AllItems
        {
            get { return _context.DeliveryDetails; }
        }

        public void SaveChanges(DeliveryDetails delivery)
        {
            if (delivery.Id == null)
            {
                _context.DeliveryDetails.Add(delivery);
            }
            else
            {
                DeliveryDetails dbEntry = _context.DeliveryDetails.Find(delivery.Id);
                if (dbEntry != null)
                {
                    dbEntry.FillCommonFields();
                    dbEntry.FirstName = delivery.FirstName;
                    dbEntry.SecondName = delivery.SecondName;
                    dbEntry.Street = delivery.Street;
                    dbEntry.City = delivery.City;
                    dbEntry.Country = delivery.Country;
                    dbEntry.Email = delivery.Email;
                    dbEntry.PhoneNumber = delivery.PhoneNumber;
                }

                _context.SaveChanges();
            }
        }

        public DeliveryDetails DeleteItem(int deliveryId)
        {
            DeliveryDetails dbEntry = _context.DeliveryDetails.Find(deliveryId);
            if (dbEntry != null)
            {
                _context.DeliveryDetails.Remove(dbEntry);
                _context.SaveChanges();
            }

            return dbEntry;
        }
    }
}