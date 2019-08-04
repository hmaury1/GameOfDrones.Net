using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using GDWebApiLayer.Controllers;

namespace GDWebApiLayer
{
    public static class AutofacDiBootstrapper
    {
        public static void Bootstrap()
        {
            var containerBuilder = new ContainerBuilder();
            SetupRegistration.Bootstrap(containerBuilder);
            containerBuilder.RegisterApiControllers(typeof(GameController).Assembly);
            containerBuilder.RegisterApiControllers(typeof(MoveController).Assembly);
            var container = containerBuilder.Build();
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
        }
    }
}
