using System;
using System.Collections.Generic;
using CompStore.Domain.Entities;

namespace CompStore.OrderHandler {
    public class OrderManager {
        private List<IOrderHandler> _handlers = new List<IOrderHandler>();
        private IOrderHandler _defaultHandler;

        public OrderManager(IOrderHandler defaultHandler) {
            if (defaultHandler == null) { throw new NullReferenceException(); }

            this._defaultHandler = defaultHandler;
        }

        public void AddHandler(IOrderHandler handler) {
            this._handlers.Add(handler);
        }

        public void ProcessNewOrder(OrderData orderData) {
            foreach (IOrderHandler handler in this._handlers) {
                if (handler.Process(orderData)) {
                    return;
                }
            }

            if (!this._defaultHandler.Process(orderData)) {
                throw new InvalidOperationException();
            }
        }
    }
}
