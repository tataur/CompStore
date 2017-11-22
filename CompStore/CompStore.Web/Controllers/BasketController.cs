using CompStore.Domain.Abstract;
using CompStore.Domain.Entities;
using CompStore.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CompStore.Web.Controllers
{
    public class BasketController : Controller
    {
        private ICompRepository repository;
        private IOrderHandler orderHandler;

        public ViewResult Index(ShoppingList shoppingList, string returnUrl)
        {
            return View(new ShoppingIndexViewModel
            {
                ShoppingList = shoppingList,
                ReturnUrl = returnUrl
            });
        }

        public BasketController(ICompRepository repo, IOrderHandler handler)
        {
            repository = repo;
            orderHandler = handler;
        }

        public ViewResult Checkout()
        {
            return View(new DeliveryDetails());
        }

        [HttpPost]
        public ViewResult Checkout(ShoppingList shoppingList, DeliveryDetails deliveryDetails)
        {
            if (shoppingList.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");
            }

            if (ModelState.IsValid)
            {
                orderHandler.HandleOrder(shoppingList, deliveryDetails);
                shoppingList.Clear();
                return View("OK");
            }
            else
            {
                return View(deliveryDetails);
            }
        }

        public RedirectToRouteResult AddToList(ShoppingList shoppingList, Guid compId, string returnUrl)
        {
            Comp comp = repository.Computers.FirstOrDefault(c => c.CompId == compId);

            if (comp != null)
            {
                shoppingList.AddItem(comp, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromList(ShoppingList shoppingList, Guid compId, string returnUrl)
        {
            Comp comp = repository.Computers.FirstOrDefault(c => c.CompId == compId);

            if (comp != null)
            {
                shoppingList.RemoveLine(comp);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(ShoppingList shoppingList)
        {
            return PartialView(shoppingList);
        }
    }
}