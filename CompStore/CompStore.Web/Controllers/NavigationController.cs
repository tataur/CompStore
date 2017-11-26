using System.Web.Mvc;
using CompStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using CompStore.Domain.Entities;

namespace CompStore.Web.Controllers
{
    public class NavigationController : Controller
    {
        private ICommonRepository<Comp> repository;

        public NavigationController(ICommonRepository<Comp> repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.AllItems.Select(comp => comp.Category).Distinct().OrderBy(c => c);
            return PartialView(categories);
        }

        public PartialViewResult MenuMobile(string category = null, bool isHorizontal = true)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.AllItems
                .Select(comp => comp.Category)
                .Distinct()
                .OrderBy(x => x);
            string viewName = isHorizontal ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, categories);
        }
    }
}