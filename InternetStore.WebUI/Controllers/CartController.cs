using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetStore.Domain.Abstract;
using InternetStore.Domain.Entities;
using InternetStore.WebUI.Models;
using log4net;
using log4net.Config;

namespace InternetStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CartController));
        private IProductRepository repository;
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnUrl)
        {
            log4net.Config.XmlConfigurator.Configure();
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            log.Info("Product add to cart");
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productID, string returnUrl)
        {
            log4net.Config.XmlConfigurator.Configure();
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            log.Info("Product delete from cart");
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}
