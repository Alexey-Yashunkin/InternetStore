using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using InternetStore.domain.Entities;
using InternetStore.domain.Abstract;
using Moq;

namespace InternetStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product { Name = "Smartphone", Price = 100 },
                new Product { Name = "NoteBook", Price = 350 },
                new Product { Name = "Mp3 Pod", Price = 33 }
            }.AsQueryable());
            ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}