using System;
using CompStore.Domain.Entities;

namespace CompStore.OrderHandler {
    public class FirstCallCustomerOrderHandler : IOrderHandler {
        public bool Process(OrderData orderData) {
            if ((orderData.Id != 0) || (orderData.CustomerId > 10)) {
                return false;
            }

            Console.WriteLine("First customer order handler.");
            orderData.Id = 77;

            return true;
        }
    }
}
