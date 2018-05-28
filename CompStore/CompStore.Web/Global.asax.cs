using CompStore.Web.Infrastructure.Binders;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CompStore.Domain.Models;

namespace CompStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(ProductList), new BasketModelBinder());
        }
    }
}