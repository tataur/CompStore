using CompStore.Domain.Entities;

namespace CompStore.Domain.Abstract
{
    public interface IOrderHandler
    {
        void HandleOrder(ShoppingList shoppingList, DeliveryDetails deliveryDetails);


        void SendMessage(ShoppingList shoppingList, DeliveryDetails deliveryDetails);
    }
}
