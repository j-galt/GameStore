using Autofac;
using Autofac.Extras.NLog;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.DAL;
using GameStore.DAL.Repositories;
using GameStore.Web.Mapping;
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
            builder.RegisterModule(new AutoMapperModule());
            builder.RegisterModule<NLogModule>();

            builder.RegisterType<GameStoreDbContext>().AsSelf().InstancePerRequest();            

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerRequest();
            builder.RegisterType<GameService>().As<IGameService>().InstancePerRequest();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerRequest();
            builder.RegisterType<PublisherService>().As<IPublisherService>().InstancePerRequest();
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerRequest();

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}