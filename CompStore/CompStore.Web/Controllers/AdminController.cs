using System.Web.Mvc;
using CompStore.Domain.Abstract;
using CompStore.Domain.Entities;
using System.Linq;

namespace CompStore.Web.Controllers
{
    public class AdminController : Controller
    {
        ICompRepository repository;

        public AdminController(ICompRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Computers);
        }

        public ViewResult Create()
        {
            return View("Edit", new Comp());
        }

        public ViewResult Edit(System.Guid compId)
        {
            Comp comp = repository.Computers.FirstOrDefault(c => c.CompId == compId);
            return View(comp);
        }

        [HttpPost]
        public ActionResult Edit(Comp comp)
        {
            if (ModelState.IsValid)
            {
                repository.SaveChanges(comp);
                TempData["message"] = string.Format("Изменения \"{0}\" были сохранены", comp.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(comp);
            }
        }

        [HttpPost]
        public ActionResult Delete(int compId)
        {
            Comp deletedComp = repository.DeleteComp(compId);
            if (deletedComp != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален", deletedComp.Name);
            }
            return RedirectToAction("Index");
        }
    }
}