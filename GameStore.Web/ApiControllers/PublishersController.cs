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

        // GET: api/Publishers
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Publishers/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("{id}/games")]
        public HttpResponseMessage GetPublisherGames(int id)
        {
            var publisher = _publService.Get(id);

            if (publisher == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var games = _mapper.Map<IEnumerable<Game>, 
                IEnumerable<GetGameResource>>(publisher.Games);

            return Request.CreateResponse(HttpStatusCode.OK, games);
        }

        // POST: api/Publishers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Publishers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Publishers/5
        public void Delete(int id)
        {
        }
    }
}
