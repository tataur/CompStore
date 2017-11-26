using CompStore.Domain.Abstract;
using CompStore.Domain.Entities;
using CompStore.Domain.Concrete;
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

        public ViewResult Index(ProductList productList, string returnUrl)
        {
            return View(new ShoppingIndexViewModel
            {
                ProductList = productList,
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
        public ViewResult Checkout(ProductList productList, DeliveryDetails deliveryDetails)
        {
            if (productList.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Корзина пуста");
            }

            if (ModelState.IsValid)
            {
                deliveryRepository.SaveChanges(deliveryDetails);
                foreach (var item in productList.Lines)
                {
                    var orderLine = new OrderLine
                    {
                        Id = Guid.NewGuid(),
                        CompId = item.Comp.Id,
                        Quantity = item.Quantity,
                        DeliveryDetailsId = deliveryDetails.Id,
                        Status = OrderStatus.Wait
                    };
                    productList.AddToDB(orderLine, deliveryDetails);
                }

                // записать в базу => передать обратчику          

                //orderHandler.HandleOrder(productList, deliveryDetails);

                productList.Clear();
                return View("OK");
            }
            else
            {
                return View(deliveryDetails);
            }
        }

        public RedirectToRouteResult AddToList(ProductList productList, Guid Id, string returnUrl)
        {
            Comp comp = compRepository.AllItems.FirstOrDefault(c => c.Id == Id);

            if (comp != null && comp.Quantity != 0)
            {
                productList.AddItem(comp, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromList(ProductList productList, Guid compId, string returnUrl)
        {
            Comp comp = compRepository.AllItems.FirstOrDefault(c => c.Id == compId);

            if (comp != null)
            {
                productList.RemoveLine(comp);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(ProductList productList)
        {
            return PartialView(productList);
        }
    }
}