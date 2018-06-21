﻿using Autofac;
using AutoMapper;
using GameStore.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.App_Start
{
    public class AutoMapperConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AutoMapperConfig).Assembly).As<MappingProfile>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                    cfg.AddProfile(profile);
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}