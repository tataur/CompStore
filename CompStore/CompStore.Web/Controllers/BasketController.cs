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
        private ICommonRepository<Comp> compRepository;
        private ICommonRepository<DeliveryDetails> deliveryRepository;
        private IOrderHandler orderHandler;

        public ViewResult Index(ShoppingList shoppingList, string returnUrl)
        {
            return View(new ShoppingIndexViewModel
            {
                ShoppingList = shoppingList,
                ReturnUrl = returnUrl
            });
        }

        public BasketController(ICommonRepository<Comp> cRepo, IOrderHandler handler)
        {
            compRepository = cRepo;
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
                deliveryRepository.SaveChanges(deliveryDetails);
                foreach (var item in shoppingList.Lines)
                {
                    var deliveryLine = new OrderLine
                    {
                        Id = Guid.NewGuid(),
                        CompId = item.Comp.CompId,
                        Quantity = item.Quantity,
                        DeliveryDetailsId = deliveryDetails.Id,
                        Status = OrderStatus.Wait
                    };
                }

                /*записать в базу => передать обратчику
                 
                class DeliveryDetails
                
                class DeliveryDetailsForEmployee 
                CompId, DeliveryId, Quantity
                
                */

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
            Comp comp = compRepository.Items.FirstOrDefault(c => c.CompId == compId);

            if (comp != null && comp.Quantity != 0)
            {
                shoppingList.AddItem(comp, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromList(ShoppingList shoppingList, Guid compId, string returnUrl)
        {
            Comp comp = compRepository.Items.FirstOrDefault(c => c.CompId == compId);

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