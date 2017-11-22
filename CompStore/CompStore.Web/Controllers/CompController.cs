using System.Linq;
using System.Web.Mvc;
using CompStore.Domain.Abstract;
using CompStore.Web.Models;

namespace CompStore.Web.Controllers
{
    public class CompController : Controller
    {
        private ICompRepository repository;
        public int pageSize = 4;

        public CompController(ICompRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            CompListViewModel model = new CompListViewModel
            {
                Computers = repository.Computers
                .Where(comp => comp.Category == null || comp.Category == category)
                .OrderBy(comp => comp.CompId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? repository.Computers.Count() : repository.Computers.Where(comp => comp.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}