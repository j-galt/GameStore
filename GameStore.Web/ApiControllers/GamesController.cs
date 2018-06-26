using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using GameStore.Web.ApiResources;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace GameStore.Web.ApiControllers
{
    [RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, ICommentService commentService, 
            IGenreService genreService,  IMapper mapper)
        {
            _gameService = gameService;
            _commentService = commentService;
            _genreService = genreService;
            _mapper = mapper;
        }

        public HttpResponseMessage GetAllGames()
        {
            var games = _gameService.GetAll();

            if (games == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var gameResources = _mapper.Map<IEnumerable<Game>, 
                IEnumerable<GetGameResource>>(games);

            return Request.CreateResponse(HttpStatusCode.OK, gameResources);
        }

        public HttpResponseMessage GetGame(int id)
        {
            var game = _gameService.Get(id);

            if (game == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var gameResource = _mapper.Map<Game, GetGameResource>(game);
            return Request.CreateResponse(HttpStatusCode.OK, gameResource);
        }

        [Route("{id}/genres")]
        public HttpResponseMessage GetGameGenres(int id)
        {
            var game = _gameService.Get(id);

            if (game == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var genreResources = _mapper.Map<IEnumerable<Genre>,
                IEnumerable<GenreResource>>(game.Genres);

            return Request.CreateResponse(HttpStatusCode.OK, genreResources);
        }

        [Route("~/api/genre/{id}/games")]
        public HttpResponseMessage GetGamesByGenre(string id)
        {
            var genre = _genreService.Get(g => g.GenreName == id, g => g.Games);

            if (genre == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var games = _mapper.Map<IEnumerable<Game>,
                IEnumerable<GetGameResource>>(genre.Games);

            return Request.CreateResponse(HttpStatusCode.OK, games);
        }

        public HttpResponseMessage CreateGame([FromBody] CreateGameResource gameResource)
        {
            if (gameResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var game = _mapper.Map<CreateGameResource, Game>(gameResource);
            _gameService.Create(game);

            var getGameRes = _mapper.Map<Game, GetGameResource>(game);
            return Request.CreateResponse(HttpStatusCode.OK, getGameRes);
        }

        [HttpPut]
        public HttpResponseMessage UpdateGame(int id, [FromBody] CreateGameResource gameResource)
        {
            if (gameResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var game = _mapper.Map<CreateGameResource, Game>(gameResource);
            _gameService.Edit(id, game);

            var getGameRes = _mapper.Map<Game, GetGameResource>(game);
            return Request.CreateResponse(HttpStatusCode.OK, getGameRes);
        }

        public HttpResponseMessage DeleteGame(int id)
        {
            var game = _gameService.Get(id);

            if (game == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            _gameService.Delete(game);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        [Route("{id}/download")]
        public HttpResponseMessage DownloadGame()
        {
            var stream = new MemoryStream();

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };

            result.Content.Headers.ContentDisposition = 
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "game.bin"
                };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }

        [Route("{id}/comments")]
        public HttpResponseMessage GetComments(int id)
        {
            var game = _gameService.Get(id);

            if (game == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var comResources = _mapper.Map<IEnumerable<Comment>, 
                IEnumerable<CommentResource>>(game.Comments);
            return Request.CreateResponse(HttpStatusCode.OK, comResources);
        }

        [HttpPost, Route("{id}/comments")]
        public HttpResponseMessage CreateComment([FromBody] CommentResource commentResource, int id)
        {
            if (commentResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var comment = _mapper.Map<CommentResource, Comment>(commentResource);
            comment.GameId = id;
            _commentService.Create(comment);

            return Request.CreateResponse(HttpStatusCode.OK, commentResource);
        }

        [HttpPost, Route("~/api/comments/{id}/comments")]
        public HttpResponseMessage CreateAnswer([FromBody] CommentResource commentResource, int id)
        {
            if (commentResource == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var comment = _mapper.Map<CommentResource, Comment>(commentResource);
            comment.ParentCommentId = id;
            _commentService.Create(comment);

            return Request.CreateResponse(HttpStatusCode.OK, commentResource);
        }
    }
}
