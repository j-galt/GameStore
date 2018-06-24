using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Services
{
    public class PublisherService : Service<Publisher>, IPublisherService
    {
        private readonly IRepository<Publisher> _publRepository;

        public PublisherService(IRepository<Publisher> publRepository, IUnitOfWork unitOfWork)
            : base(publRepository, unitOfWork)
        {
            _publRepository = publRepository;
        }

        public Publisher Get(int id)
        {
            return base.Get(p => p.PublisherId == id, p => p.Games);
        }

        public override Publisher Edit(int id, Publisher updatedEntity)
        {
            throw new NotImplementedException();
        }

        public Publisher GetPublisher(int id)
        {
            return _publRepository.Get(id);
        }
    }
}
