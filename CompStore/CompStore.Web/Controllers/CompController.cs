using System.Linq;
using System.Web.Mvc;
using CompStore.Domain.Abstract;
using CompStore.Web.Models;
using System;
using CompStore.Domain.Entities;
using CompStore.Domain.Concrete;

namespace CompStore.Web.Controllers
{
    public class CompController : Controller
    {
        private readonly EFDbContext context = new EFDbContext();
        public int pageSize = 5;

        public ViewResult List(string category, int page = 1)
        {
            CompListViewModel model = new CompListViewModel
            {
                Computers = context.Computers
                .Where(comp => comp.Category == null || comp.Category == category)
                .OrderBy(comp => comp.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? context.Computers.Count() : context.Computers.Where(comp => comp.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public ViewResult Details(Guid id)
        {
            var model = context.Computers.FirstOrDefault(c => c.Id == id);

            return View(model);
        }
    }
}