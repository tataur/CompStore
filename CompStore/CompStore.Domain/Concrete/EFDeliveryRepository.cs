using System.Collections.Generic;
using CompStore.Domain.Entities;
using CompStore.Domain.Abstract;

namespace CompStore.Domain.Concrete
{
    public class EFDeliveryRepository : ICommonRepository<DeliveryDetails>
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<DeliveryDetails> AllItems
        {
            get { return context.DeliveryDetails; }
        }

        public void SaveChanges(DeliveryDetails delivery)
        {
            if (delivery.Id == null)
            {
                context.DeliveryDetails.Add(delivery);
            }
            else
            {
                DeliveryDetails dbEntry = context.DeliveryDetails.Find(delivery.Id);
                if (dbEntry != null)
                {
                    dbEntry.FillCommonFields();
                    dbEntry.FirstName = delivery.FirstName;
                    dbEntry.SecondName = delivery.SecondName;
                    dbEntry.City = delivery.City;
                    dbEntry.Country = delivery.Country;
                    dbEntry.Email = delivery.Email;
                    dbEntry.PhoneNumber = delivery.PhoneNumber;
                    dbEntry.Line = delivery.Line;
                }
                context.SaveChanges();
            }
        }

        public DeliveryDetails DeleteItem(int deliveryId)
        {
            DeliveryDetails dbEntry = context.DeliveryDetails.Find(deliveryId);
            if (dbEntry != null)
            {
                context.DeliveryDetails.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
