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

        public HttpResponseMessage GetAllGenres()
        {
            var genres = _genreService.GetAll();

            if (genres == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var gameResources = _mapper.Map<IEnumerable<Genre>,
                IEnumerable<GenreResource>>(genres);

            return Request.CreateResponse(HttpStatusCode.OK, gameResources);
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

        public HttpResponseMessage GetGenre(string name)
        {
            var genre = _genreService.Get(g => g.GenreName == name);

            if (genre == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var gameResource = _mapper.Map<Genre, GenreResource>(genre);
            return Request.CreateResponse(HttpStatusCode.OK, gameResource);
        }

        public HttpResponseMessage CreateGenre([FromBody] GenreResource genreResource)
        {
            if (genreResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var genre = _mapper.Map<GenreResource, Genre>(genreResource);
            _genreService.Create(genre);

            var getGameRes = _mapper.Map<Genre, GenreResource>(genre);
            return Request.CreateResponse(HttpStatusCode.OK, getGameRes);
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public HttpResponseMessage DeleteGenre(string name)
        {
            var genre = _genreService.Get(g => g.GenreName == name);

            if (genre == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            _genreService.Delete(genre);
            return Request.CreateResponse(HttpStatusCode.OK, name);
        }
    }
}
