using System.Linq;
using System.Web.Mvc;
using CompStore.Domain.Abstract;
using CompStore.Web.Models;
using System;
using CompStore.Domain.Entities;

namespace CompStore.Web.Controllers
{
    public class CompController : Controller
    {
        private ICommonRepository<Comp> repository;
        public int pageSize = 5;

        public CompController(ICommonRepository<Comp> repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            CompListViewModel model = new CompListViewModel
            {
                Computers = repository.AllItems
                .Where(comp => comp.Category == null || comp.Category == category)
                .OrderBy(comp => comp.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? repository.AllItems.Count() : repository.AllItems.Where(comp => comp.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public ViewResult Details(Guid id)
        {
            var model = repository.AllItems.FirstOrDefault(c => c.Id == id);

            return View(model);
        }
    }
}