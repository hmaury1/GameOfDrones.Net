using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using GameOfDronesContractsLayer.Contracts.Interfaces;
using GameOfDronesDataAccessLayer.DataAccess;
using GameOfDronesDataAccessLayer.DataAccess.Interfaces;
using GameOfDronesDataAccessLayer.DataAccess.Repositories;
using GameOfDronesDataAccessLayer.Implementations;
using GameOfDronesWebApiLayer.Controllers;

namespace GameOfDronesWebApiLayer
{
    public static class AutofacDiBootstrapper
    {
        public static void Bootstrap()
        {
            var containerBuilder = new ContainerBuilder();
            SetupRegistration.Bootstrap(containerBuilder);
            containerBuilder.RegisterApiControllers(typeof(EmployeeController).Assembly);
            containerBuilder.RegisterApiControllers(typeof(GameController).Assembly);
            var container = containerBuilder.Build();
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
        }
    }
}
