using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using GameStore.Web.ApiResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameStore.Web.ApiControllers
{
    [RoutePrefix("api/publishers")]
    public class PublishersController : ApiController
    {
        private readonly IPublisherService _publService;
        private readonly IMapper _mapper;

        public PublishersController(IPublisherService publService, IMapper mapper)
        {
            _publService = publService;
            _mapper = mapper;
        }

        public HttpResponseMessage GetAllPublishers()
        {
            var publishers = _publService.GetAll();

            if (publishers == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var publResources = _mapper.Map<IEnumerable<Publisher>,
                IEnumerable<PublisherResource>>(publishers);

            return Request.CreateResponse(HttpStatusCode.OK, publResources);
        }

        public HttpResponseMessage GetPublisher(int id)
        {
            var publisher = _publService.Get(p => p.PublisherId == id);

            if (publisher == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var publResource = _mapper.Map<Publisher, PublisherResource>(publisher);
            return Request.CreateResponse(HttpStatusCode.OK, publResource);
        }

        [Route("{id}/games")]
        public HttpResponseMessage GetPublisherGames(int id)
        {
            var publisher = _publService.Get(p => p.PublisherId == id);

            if (publisher == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var games = _mapper.Map<IEnumerable<Game>, 
                IEnumerable<GetGameResource>>(publisher.Games);

            return Request.CreateResponse(HttpStatusCode.OK, games);
        }

        public HttpResponseMessage CreatePublisher([FromBody] PublisherResource publResource)
        {
            if (publResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var publisher = _mapper.Map<PublisherResource, Publisher>(publResource);
            _publService.Create(publisher);

            var publRes = _mapper.Map<Publisher, PublisherResource>(publisher);
            return Request.CreateResponse(HttpStatusCode.OK, publRes);
        }

        public HttpResponseMessage EditPublisher(int id, [FromBody] PublisherResource publResource)
        {
            if (publResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var publisher = _mapper.Map<PublisherResource, Publisher>(publResource);
            _publService.Edit(id, publisher);

            var publRes = _mapper.Map<Publisher, PublisherResource>(publisher);
            return Request.CreateResponse(HttpStatusCode.OK, publRes);
        }

        public HttpResponseMessage Delete(int id)
        {
            var publisher = _publService.Get(p => p.PublisherId == id);

            if (publisher == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            _publService.Delete(publisher);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}
