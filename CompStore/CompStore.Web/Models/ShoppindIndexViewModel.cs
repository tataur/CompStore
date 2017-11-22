using CompStore.Domain.Entities;

namespace CompStore.Web.Models
{
    public class CartIndexViewModel
    {
        public ShoppingList ShoppingList { get; set; }
        public string ReturnUrl { get; set; }
    }
}