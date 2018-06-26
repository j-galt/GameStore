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
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenresController(IGenreService genreService, IMapper mapper)
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

        public HttpResponseMessage EditGenre(int id, [FromBody] GenreResource genreResource)
        {
            if (genreResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var genre = _mapper.Map<GenreResource, Genre>(genreResource);
            _genreService.Edit(id, genre);

            var genreRes = _mapper.Map<Genre, GenreResource>(genre);
            return Request.CreateResponse(HttpStatusCode.OK, genreRes);
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
