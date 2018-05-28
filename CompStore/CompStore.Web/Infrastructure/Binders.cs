using System.Web.Mvc;
using CompStore.Domain.Models;

namespace CompStore.Web.Infrastructure.Binders
{
    public class BasketModelBinder : IModelBinder
    {
        private const string SessionKey = "ShoppingList";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ProductList shoppingList = null;
            if (controllerContext.HttpContext.Session != null)
            {
                shoppingList = (ProductList) controllerContext.HttpContext.Session[SessionKey];
            }

            if (shoppingList == null)
            {
                shoppingList = new ProductList();
                controllerContext.HttpContext.Session[SessionKey] = shoppingList;
            }

            return shoppingList;
        }
    }
}