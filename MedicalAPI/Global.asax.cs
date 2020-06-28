using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MedicalAPI.Controllers;
using MedicalAPI.Filter;
using MedicalAPI.Interface;
using MedicalEF6;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MedicalAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;
            // auto fac
            var builder = new ContainerBuilder();
            // Usually you're only interested in exposing the type
            // via its interface:


            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterInstance(new MedicalContext()).As<DbContext>();
            builder.RegisterInstance(new Class1()).As<IBase>();
            //builder.RegisterType<MedicalContext>().as

            builder.Register(c => new TransactionFilter(c.Resolve<DbContext>()))
    .AsWebApiActionFilterFor<ValuesController>(c => c.Post(default(string)))
    .InstancePerRequest();

            IContainer container =   builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            


        }

    }
}
