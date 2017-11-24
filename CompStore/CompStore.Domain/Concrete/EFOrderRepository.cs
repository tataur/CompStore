using System.Collections.Generic;
using CompStore.Domain.Entities;
using CompStore.Domain.Abstract;

namespace CompStore.Domain.Concrete
{
    public class EFOrderRepository : ICommonRepository<OrderLine>
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<OrderLine> Items
        {
            get { return context.OrderLine; }
        }

        public void SaveChanges(OrderLine order)
        {
            if (order.Id == null)
            {
                context.OrderLine.Add(order);
            }
            else
            {
                OrderLine dbEntry = context.OrderLine.Find(order.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = order.Id;
                    dbEntry.CompId = order.CompId;
                    dbEntry.DeliveryDetailsId = order.DeliveryDetailsId;
                    dbEntry.Quantity = order.Quantity;
                    dbEntry.Status = OrderStatus.Delivery;
                }
                context.SaveChanges();
            }
        }

        public OrderLine Delete(int orderId)
        {
            OrderLine dbEntry = context.OrderLine.Find(orderId);
            if (dbEntry != null)
            {
                context.OrderLine.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
