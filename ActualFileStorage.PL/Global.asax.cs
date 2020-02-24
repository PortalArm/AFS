using ActualFileStorage.BLL.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace ActualFileStorage.PL
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            IUnityContainer cont = new IoCContainer();//new UnityContainer();
            //cont.

            //cont.RegisterInstance<IUnityContainer>(cont);
            DIControllerFactory factory = new DIControllerFactory(cont); //cont.Resolve<DIControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(factory);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
