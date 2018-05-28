using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using CompStore.DAL.Context;

namespace CompStore.Web.Controllers
{
    public class NavigationController : Controller
    {
        private readonly EFDbContext _context = new EFDbContext();

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _context.Computers.Select(comp => comp.Category).Distinct().OrderBy(c => c);
            return PartialView(categories);
        }

        public PartialViewResult MenuMobile(string category = null, bool isHorizontal = true)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _context.Computers
                .Select(comp => comp.Category)
                .Distinct()
                .OrderBy(x => x);
            string viewName = isHorizontal ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, categories);
        }
    }
}