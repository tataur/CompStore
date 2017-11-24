using System.Web.Mvc;
using CompStore.Domain.Abstract;
using CompStore.Domain.Entities;
using System.Linq;

namespace CompStore.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ICommonRepository<Comp> repository;

        public AdminController(ICommonRepository<Comp> repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Items);
        }

        public ViewResult Create()
        {
            return View("Edit", new Comp());
        }

        public ViewResult Edit(System.Guid compId)
        {
            Comp comp = repository.Items.FirstOrDefault(c => c.CompId == compId);
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
            Comp deletedComp = repository.Delete(compId);
            if (deletedComp != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален", deletedComp.Name);
            }
            return RedirectToAction("Index");
        }
    }
}