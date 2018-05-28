using System;
using CompStore.Domain.Entities;

namespace CompStore.OrderHandler
{
    public interface IOrderHandler
    {
        // найти незанятого сборщика Worker (статус заказа Wait, сборщика Wait)

        // передать заказ сборщику, изменить статус заказа и сборщика (статус заказа Work, сборщика Work)

        // после сборки заказа изменить статус заказа и сборщика (статус заказа WorkDone, сборщика Wait)

        // передать его в доставку Deliveryman (статус заказа Delivery, доставщика Work)

        // после доставки изменить статус заказа и доставщика, (статус заказа Done, доставщика Wait) 

        // отправить сообщение заказчику
        //void SendMessage(Guid productListId, Guid deliveryDetailsId);

        bool Process(OrderData orderData);
    }
}