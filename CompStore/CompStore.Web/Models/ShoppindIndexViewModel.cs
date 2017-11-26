using CompStore.Domain.Concrete;

namespace CompStore.Web.Models
{
    public class ShoppingIndexViewModel
    {
        public ProductList ProductList { get; set; }
        public string ReturnUrl { get; set; }
    }
}