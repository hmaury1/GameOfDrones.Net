using System;
using System.Configuration;
using Autofac;
using GameOfDronesContractsLayer.Contracts.Interfaces;
using GameOfDronesDataAccessLayer.DataAccess;
using GameOfDronesDataAccessLayer.DataAccess.Interfaces;
using GameOfDronesDataAccessLayer.DataAccess.Repositories;
using GameOfDronesDataAccessLayer.Implementations;

namespace GameOfDronesWebApiLayer
{
    public static class SetupRegistration
    {
        internal static void Bootstrap(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AppContext>()
                .InstancePerRequest()
                .UsingConstructor(typeof(string))
                .WithParameter("connectName", ConfigurationManager.ConnectionStrings["SampleAppConnection"].ConnectionString);

            containerBuilder.RegisterGeneric(typeof(BaseRepository<,>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            containerBuilder.RegisterType<GameRepository>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PropertiesAutowired();

            containerBuilder.RegisterType<PlayerRepository>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PropertiesAutowired();

            containerBuilder.RegisterType<MoveRepository>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PropertiesAutowired();

            containerBuilder.RegisterType<RoundRepository>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PropertiesAutowired();

            containerBuilder.RegisterType<DepartmentRepository>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PropertiesAutowired();

            containerBuilder.RegisterType<EmployeeRepository>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PropertiesAutowired();

            containerBuilder.RegisterType<AppService>()
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }
    }
}