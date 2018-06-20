using Autofac;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Util
{
    public class DataModule : Module
    {
        private string _connStr;

        public DataModule(string connStr)
        {
            _connStr = connStr;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new GameStoreDbContext(_connStr)).InstancePerRequest();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
