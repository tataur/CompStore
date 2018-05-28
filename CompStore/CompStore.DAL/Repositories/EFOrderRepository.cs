using System.Collections.Generic;
using CompStore.Domain.Entities;
using CompStore.Domain.Interfaces;
using CompStore.DAL.Context;

namespace CompStore.DAL.Repositories {
    public class EFOrderRepository : ICommonRepository<OrderLine>
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IEnumerable<OrderLine> AllItems
        {
            get { return _context.OrderLines; }
        }

        public void SaveChanges(OrderLine order)
        {
            if (order.Id == null)
            {
                _context.OrderLines.Add(order);
            }
            else
            {
                OrderLine dbEntry = _context.OrderLines.Find(order.Id);
                if (dbEntry != null)
                {
                    dbEntry.FillCommonFields();
                    dbEntry.CompId = order.CompId;
                    dbEntry.DeliveryDetailsId = order.DeliveryDetailsId;
                    dbEntry.Quantity = order.Quantity;
                    dbEntry.Status = OrderStatus.Delivery;
                }

                _context.SaveChanges();
            }
        }

        public OrderLine DeleteItem(int orderId)
        {
            OrderLine dbEntry = _context.OrderLines.Find(orderId);
            if (dbEntry != null)
            {
                _context.OrderLines.Remove(dbEntry);
                _context.SaveChanges();
            }

            return dbEntry;
        }
    }
}