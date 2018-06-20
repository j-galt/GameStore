using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.DAL;
using GameStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GameStore.Web.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterComponents()
        {            
            var builder = new ContainerBuilder();
            var configuration = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<GameService>().As<IGameService>().InstancePerRequest();

            builder.RegisterType<GameStoreDbContext>().AsSelf().InstancePerRequest();

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

    }
}