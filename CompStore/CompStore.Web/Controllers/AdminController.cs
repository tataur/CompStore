using System.Web.Mvc;
using System.Linq;
using System;
using CompStore.DAL.Context;
using CompStore.Domain.Entities;

namespace CompStore.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly EFDbContext _context = new EFDbContext();

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Computers()
        {
            return View(_context.Computers);
        }

        public ViewResult Employees()
        {
            return View(_context.Employees);
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
            Comp comp = _context.Computers.FirstOrDefault(c => c.Id == Id);
            return View(comp);
        }

        [HttpPost]
        public ActionResult EditComputer(Comp comp)
        {
            if (ModelState.IsValid)
            {
                var compExist = _context.Computers.FirstOrDefault(c => c.Id == comp.Id);
                if (compExist == null)
                {
                    comp.FillCommonFields();
                    _context.Computers.Add(comp);
                    _context.SaveChanges();
                    TempData["message"] = string.Format("Изменения \"{0}\" были сохранены", comp.Name);
                }
                else
                {
                    compExist.Name = comp.Name;
                    compExist.Price = comp.Price;
                    compExist.Quantity = comp.Quantity;
                    compExist.Category = comp.Category;
                    compExist.Description = comp.Description;
                    _context.SaveChanges();
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
            Employee employee = _context.Employees.FirstOrDefault(c => c.Id == Id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employeeExist = _context.Employees.FirstOrDefault(c => c.Id == employee.Id);
                if (employeeExist == null)
                {
                    employee.FillCommonFields();
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    TempData["message"] = string.Format("Изменения \"{0}\" были сохранены", employee.GetFullName());
                }
                else
                {
                    employeeExist.FirstName = employee.FirstName;
                    employeeExist.SecondName = employee.SecondName;
                    employeeExist.Salary = employee.Salary;
                    employeeExist.Category = employee.Category;
                    employeeExist.Status = employee.Status;
                    _context.SaveChanges();
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
            Comp deletedComp = _context.Computers.FirstOrDefault(c => c.Id == Id);
            if (deletedComp != null)
            {
                _context.Computers.Remove(deletedComp);
                TempData["message"] = string.Format("Товар \"{0}\" был удален", deletedComp.Name);
            }

            return RedirectToAction("Computers");
        }

        [HttpPost]
        public ActionResult DeleteEmployee(Guid Id)
        {
            Employee deletedEmployee = _context.Employees.FirstOrDefault(e => e.Id == Id);
            if (deletedEmployee != null)
            {
                _context.Employees.Remove(deletedEmployee);
                TempData["message"] = string.Format("Товар \"{0}\" был удален", deletedEmployee.GetFullName());
            }

            return RedirectToAction("Employees");
        }
    }
}