using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetStore.Domain.Abstract;
using InternetStore.Domain.Entities;
using InternetStore.WebUI.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace InternetStore.WebUI.Controllers
{
    [Authorize(Roles = "Buyer")]
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        public OrderController(IOrderRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Index()
        {
            Order order = repository.Orders.OrderByDescending(x => x.OrderID).FirstOrDefault(x => x.UserID == WebSecurity.CurrentUserId);
            return View(order);
        }

        //[HttpPost] 
        //public ActionResult Index(Order order)
        //{
        //    return RedirectToAction("PayOrder");
        //}

        public ActionResult MakeOrder()
        {
            return View(new Order());
        }
        [HttpPost]
        public ActionResult MakeOrder(Order order, Cart cart, OrderPayment orderPayment, OrderDelivery orderDelivery)
        {   
            if (cart.Lines.Count() != 0)
            {
                order.Payment = orderPayment.Payment;
                order.Delivery = orderDelivery.Delivery;
                order.Cart = JsonConvert.SerializeObject(cart);
                order.TotalPrice = cart.ComputeTotalValue();
                order.UserID = WebSecurity.CurrentUserId;
                {
                    if (ModelState.IsValid)
                    {
                        repository.AddOrder(order);
                    }
                }
                cart.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult PayOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                order.PayStatus = "Pay";
                switch (order.Delivery)
                {
                    case "OneSelf":
                        order.OrderStatus = "Expects the buyer";
                        break;
                    case "InHome":
                        order.OrderStatus = "Expects delivery";
                        break;
                };
                repository.UpdatePayStatus(order.OrderID, order.PayStatus);
                repository.UpdateOrderStatus(order.OrderID, order.OrderStatus);
                    return RedirectToAction("Index");
            }
            else
            {
               return View(order);
            }

        }

        public ActionResult PayOrder(int? orderID)
        {
            Order order = repository.Orders.FirstOrDefault(x => x.OrderID == orderID);
            return View(order);
        }


        public ActionResult PrivateOffice()
        {
            return View(repository.Orders);
        }

        public ActionResult BuyerCart(int orderID)
        {
            ViewData["orderID"] = orderID;
            Order order = repository.Orders.FirstOrDefault(x => x.OrderID == orderID);
            Cart cart = JsonConvert.DeserializeObject<Cart>(order.Cart);
            return View(cart);
        }

        [HttpPost]
        public ActionResult CancelOrder(int orderID)
        {

            Order order = repository.Orders.FirstOrDefault(x => x.OrderID == orderID);
            if (order != null && order.PayStatus != "Pay")
            {
                repository.DeleteOrder(order.OrderID);
                TempData["message"] = string.Format("Order #{0} was deleted", order.OrderID);
            }
            else
            {
                TempData["message"] = string.Format("Заказ оплачен, отмена невозможна");
            }
            return RedirectToAction("PrivateOffice");
        }
    }
}
