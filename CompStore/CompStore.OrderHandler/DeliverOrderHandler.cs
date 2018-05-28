using System;
using CompStore.Domain.Entities;

namespace CompStore.OrderHandler {
    public class DeliverOrderHandler : IOrderHandler {
        public bool Process(OrderData orderData) {
            if ((orderData.Id != 0) || (orderData.Amount < 20)) {
                return false;
            }

            Console.WriteLine("Deliver order handler.");
            orderData.Id = 42;

            return true;
        }
    }
}
