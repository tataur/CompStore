using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using CompStore.Domain.Abstract;
using CompStore.Domain.Concrete;
using System.Configuration;
using CompStore.Domain.Entities;

namespace CompStore.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ICommonRepository<Comp>>().To<EFCompRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderHandler>().To<EmailOrderHandler>()
                .WithConstructorArgument("settings", emailSettings);

            kernel.Bind<IAuthenticate>().To<FormAuthenticate>();
        }
    }
}