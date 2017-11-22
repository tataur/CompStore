using CompStore.Domain.Entities;

namespace CompStore.Web.Models
{
    public class ShoppingIndexViewModel
    {
        public ShoppingList ShoppingList { get; set; }
        public string ReturnUrl { get; set; }
    }
}