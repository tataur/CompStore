using System.Web.Mvc;
using CompStore.Domain.Abstract;
using CompStore.Domain.Entities;
using System.Linq;
using CompStore.Domain.Concrete;
using System;

namespace CompStore.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly EFDbContext context = new EFDbContext();

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Computers()
        {
            return View(context.Computers);
        }

        public ViewResult Employees()
        {
            return View(context.Employees);
        }

        public ViewResult CreateComputer()
        {
            return View("Edit", new Comp());
        }

        public ViewResult CreateEmployee()
        {
            return View("EditEmployee", new Employee());
        }

        public ViewResult EditComputer(Guid Id)
        {
            Comp comp = context.Computers.FirstOrDefault(c => c.Id == Id);
            return View(comp);
        }

        [HttpPost]
        public ActionResult EditComputer(Comp comp)
        {
            if (ModelState.IsValid)
            {
                var compExist = context.Computers.FirstOrDefault(c => c.Id == comp.Id);
                if (compExist == null)
                {
                    comp.FillCommonFields();
                    context.Computers.Add(comp);
                    context.SaveChanges();
                    TempData["message"] = string.Format("Изменения \"{0}\" были сохранены", comp.Name);
                }
                else
                {
                    compExist.Name = comp.Name;
                    compExist.Price = comp.Price;
                    compExist.Quantity = comp.Quantity;
                    compExist.Category = comp.Category;
                    compExist.Description = comp.Description;
                    context.SaveChanges();
                    TempData["message"] = string.Format("Изменения \"{0}\" были сохранены", comp.Name);
                }
                return RedirectToAction("Computers");
            }
            else
            {
                return View(comp);
            }
        }

        public ViewResult EditEmployee(Guid Id)
        {
            Employee employee = context.Employees.FirstOrDefault(c => c.Id == Id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employeeExist = context.Employees.FirstOrDefault(c => c.Id == employee.Id);
                if (employeeExist == null)
                {
                    employee.FillCommonFields();
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    TempData["message"] = string.Format("Изменения \"{0}\" были сохранены", employee.GetFullName());
                }
                else
                {
                    employeeExist.FirstName = employee.FirstName;
                    employeeExist.SecondName = employee.SecondName;
                    employeeExist.Salary = employee.Salary;
                    employeeExist.Category = employee.Category;
                    employeeExist.Status = employee.Status;
                    context.SaveChanges();
                    TempData["message"] = string.Format("Изменения \"{0}\" были сохранены", employee.GetFullName());
                }
                return RedirectToAction("Employees");
            }
            else
            {
                return View(employee);
            }
        }

        [HttpPost]
        public ActionResult DeleteComputer(Guid Id)
        {
            Comp deletedComp = context.Computers.FirstOrDefault(c=>c.Id == Id);
            if (deletedComp != null)
            {
                context.Computers.Remove(deletedComp);
                TempData["message"] = string.Format("Товар \"{0}\" был удален", deletedComp.Name);
            }
            return RedirectToAction("Computers");
        }

        [HttpPost]
        public ActionResult DeleteEmployee(Guid Id)
        {
            Employee deletedEmployee = context.Employees.FirstOrDefault(e => e.Id == Id);
            if (deletedEmployee != null)
            {
                context.Employees.Remove(deletedEmployee);
                TempData["message"] = string.Format("Товар \"{0}\" был удален", deletedEmployee.GetFullName());
            }
            return RedirectToAction("Employees");
        }
    }
}