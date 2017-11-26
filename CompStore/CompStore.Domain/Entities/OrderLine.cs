using System;

namespace CompStore.Domain.Entities
{
    public class OrderLine
    {
        public Guid Id { get; set; }
        public Guid DeliveryDetailsId { get; set; }
        public Guid CompId { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Wait, Work, WorkDone, Delivery, Done
    }
}
