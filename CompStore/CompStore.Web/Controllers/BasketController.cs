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
        private readonly EFDbContext context = new EFDbContext(); 

        public ViewResult Index(ProductList productList, string returnUrl)
        {
            return View(new ShoppingIndexViewModel
            {
                ProductList = productList,
                ReturnUrl = returnUrl
            });
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
                deliveryDetails.FillCommonFields();
                context.DeliveryDetails.Add(deliveryDetails);
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
                    orderLine.FillCommonFields();
                    orderLine.Status = OrderStatus.Wait;
                    context.OrderLines.Add(orderLine);
                }

                // записать в базу => передать обратчику          
                context.SaveChanges();
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
            Comp comp = context.Computers.FirstOrDefault(c => c.Id == Id);

            if (comp != null && comp.Quantity != 0)
            {
                productList.AddItem(comp, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromList(ProductList productList, Guid compId, string returnUrl)
        {
            Comp comp = context.Computers.FirstOrDefault(c => c.Id == compId);

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