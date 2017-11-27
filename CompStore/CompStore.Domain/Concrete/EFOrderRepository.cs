using System.Collections.Generic;
using CompStore.Domain.Entities;
using CompStore.Domain.Abstract;

namespace CompStore.Domain.Concrete
{
    public class EFOrderRepository : ICommonRepository<OrderLine>
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<OrderLine> AllItems
        {
            get { return context.OrderLines; }
        }

        public void SaveChanges(OrderLine order)
        {
            if (order.Id == null)
            {
                context.OrderLines.Add(order);
            }
            else
            {
                OrderLine dbEntry = context.OrderLines.Find(order.Id);
                if (dbEntry != null)
                {
                    dbEntry.FillCommonFields();
                    dbEntry.CompId = order.CompId;
                    dbEntry.DeliveryDetailsId = order.DeliveryDetailsId;
                    dbEntry.Quantity = order.Quantity;
                    dbEntry.Status = OrderStatus.Delivery;
                }
                context.SaveChanges();
            }
        }

        public OrderLine DeleteItem(int orderId)
        {
            OrderLine dbEntry = context.OrderLines.Find(orderId);
            if (dbEntry != null)
            {
                context.OrderLines.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
