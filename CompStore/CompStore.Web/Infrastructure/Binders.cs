using System.Web.Mvc;
using CompStore.Domain.Concrete;

namespace CompStore.Web.Infrastructure.Binders
{
    public class BasketModelBinder : IModelBinder
    {
        private const string sessionKey = "ShoppingList";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ProductList shoppingList = null;
            if (controllerContext.HttpContext.Session != null)
            {
                shoppingList = (ProductList)controllerContext.HttpContext.Session[sessionKey];
            }

            if (shoppingList == null)
            {
                shoppingList = new ProductList();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = shoppingList;
                }
            }

            return shoppingList;
        }
    }
}