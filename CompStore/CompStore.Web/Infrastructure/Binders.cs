using System.Web.Mvc;
using CompStore.Domain.Entities;

namespace CompStore.Web.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "ShoppingList";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            ShoppingList shoppingList = null;
            if (controllerContext.HttpContext.Session != null)
            {
                shoppingList = (ShoppingList)controllerContext.HttpContext.Session[sessionKey];
            }

            if (shoppingList == null)
            {
                shoppingList = new ShoppingList();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = shoppingList;
                }
            }

            return shoppingList;
        }
    }
}