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
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var gameResources = _mapper.Map<IEnumerable<Game>, IEnumerable<GetGameResource>>(games);
            return Request.CreateResponse(HttpStatusCode.OK, gameResources);
        }

        public HttpResponseMessage Get(int id)
        {
            var game = _gameService.GetGame(id);

            if (game == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var gameResource = _mapper.Map<Game, GetGameResource>(game);
            return Request.CreateResponse(HttpStatusCode.OK, gameResource);
        }

        public HttpResponseMessage Post([FromBody] CreateGameResource gameResource)
        {
            if (gameResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var game = _mapper.Map<CreateGameResource, Game>(gameResource);
            _gameService.CreateGame(game);

            var getGameRes = _mapper.Map<Game, GetGameResource>(game);
            return Request.CreateResponse(HttpStatusCode.OK, getGameRes);
        }

        public HttpResponseMessage Put(int id, [FromBody] CreateGameResource gameResource)
        {
            if (gameResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var game = _mapper.Map<CreateGameResource, Game>(gameResource);
            _gameService.EditGame(id, game);

            var getGameRes = _mapper.Map<Game, GetGameResource>(game);
            return Request.CreateResponse(HttpStatusCode.OK, getGameRes);
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
