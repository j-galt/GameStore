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
    [RoutePrefix("api/genres")]
    public class GenresController : ApiController
    {
        private readonly IService<Genre> _genreService;
        private readonly IMapper _mapper;

        public GenresController(IService<Genre> genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        // GET: api/Genres
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("{id}/games")]
        public HttpResponseMessage GetGamesByGenre(string id)
        {
            var genre = _genreService.Get(g => g.GenreName == id, g => g.Games);

            if (genre == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var games = _mapper.Map<IEnumerable<Game>,
                IEnumerable<GetGameResource>>(genre.Games);

            return Request.CreateResponse(HttpStatusCode.OK, games);
        }

        // GET: api/Genres/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Genres
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Genres/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Genres/5
        public void Delete(int id)
        {
        }
    }
}
