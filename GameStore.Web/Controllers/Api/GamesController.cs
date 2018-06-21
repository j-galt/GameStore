using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using GameStore.Web.ApiResources;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameStore.Web.Controllers.Api
{
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        public HttpResponseMessage Get()
        {
            var games = _gameService.GetAllGames();

            if (games == null)
                NotFound();

            var gameResources = _mapper.Map<IEnumerable<Game>, IEnumerable<GameResource>>(games);
            return Request.CreateResponse(HttpStatusCode.OK, gameResources);
        }

        public HttpResponseMessage Get(int id)
        {
            var game = _gameService.GetGame(id);

            if (game == null)
                NotFound();

            return Request.CreateResponse(HttpStatusCode.OK, game);
        }

        public HttpResponseMessage Post([FromBody] Game game)
        {
            if (game == null)
                NotFound();

            if (!ModelState.IsValid)
                BadRequest(ModelState);

            _gameService.CreateGame(game);
            return Request.CreateResponse(HttpStatusCode.OK, game);
        }

        public HttpResponseMessage Put(int id, [FromBody] Game game)
        {
            if (game == null)
                NotFound();

            if (!ModelState.IsValid)
                BadRequest(ModelState);

            _gameService.EditGame(id, game);
            return Request.CreateResponse(HttpStatusCode.OK, game);
        }

        public HttpResponseMessage Delete(int id)
        {
            var game = _gameService.GetGame(id);

            if (game == null)
                NotFound();

            _gameService.DeleteGame(game);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}
