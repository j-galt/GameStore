using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using GameStore.Web.ApiResources;
using GameStore.Web.Infrastructure;
using Autofac.Extras.NLog;
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
        public ILogger _logger;

        public GamesController(IGameService gameService, ICommentService commentService, 
            IGenreService genreService, IMapper mapper, ILogger logger)
        {
            _gameService = gameService;
            _commentService = commentService;
            _genreService = genreService;
            _mapper = mapper;
            _logger = logger;
        }

        public IHttpActionResult GetAllGames()
        {
            var games = _gameService.GetAll().ToList();

            if (games == null)
                return NotFound();

            var gameResources = _mapper.Map<IEnumerable<Game>, 
                IEnumerable<GetGameResource>>(games);

            return Ok(gameResources);
        }

        [TrackUserIP]
        public IHttpActionResult GetGame(int id)
        {
            var s = HttpContext.Current.Request.UserHostAddress;
            var game = _gameService.Get(id);

            if (game == null)
            {
                _logger.Warn(LogMessageComposer.Compose(new
                {
                    details = "Entity not found in db",
                    user = "Anonimous",
                    entity = typeof(Game).Name,
                    id
                }));

                return NotFound();
            }

            var gameResource = _mapper.Map<Game, GetGameResource>(game);
            return Ok(gameResource);
        }

        [Route("{id}/genres")]
        public IHttpActionResult GetGameGenres(int id)
        {
            var game = _gameService.Get(id);

            if (game == null)
                return NotFound();

            var genreResources = _mapper.Map<IEnumerable<Genre>,
                IEnumerable<GenreResource>>(game.Genres);

            return Ok(genreResources);
        }

        [Route("~/api/genre/{id}/games")]
        public IHttpActionResult GetGamesByGenre(string id)
        {
            var genre = _genreService.Get(id);

            if (genre == null)
                return NotFound();

            var games = _mapper.Map<IEnumerable<Game>,
                IEnumerable<GetGameResource>>(genre.Games);

            return Ok(games);
        }

        public IHttpActionResult CreateGame([FromBody] CreateGameResource gameResource)
        {
            if (gameResource == null)
            {
                _logger.Warn(LogMessageComposer.Compose(new
                {
                    details = "Serialization error.",
                    user = "Anonymous"
                }));

                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var game = _mapper.Map<CreateGameResource, Game>(gameResource);
            _gameService.Create(game);

            var getGameRes = _mapper.Map<Game, GetGameResource>(game);
            return Ok(getGameRes);
        }

        [HttpPut]
        public IHttpActionResult UpdateGame(int id, [FromBody] CreateGameResource gameResource)
        {
            if (gameResource == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var game = _mapper.Map<CreateGameResource, Game>(gameResource);
            _gameService.Edit(id, game);

            var getGameRes = _mapper.Map<Game, GetGameResource>(game);
            return Ok(getGameRes);
        }

        public IHttpActionResult DeleteGame(int id)
        {
            var game = _gameService.Get(id);

            if (game == null)
                return NotFound();

            _gameService.Delete(game);
            return Ok(id);
        }

        [Route("{id}/download")]
        [HttpGet]
        public IHttpActionResult DownloadGame()
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

            return Ok(result);
        }

        [Route("{id}/comments")]
        public IHttpActionResult GetComments(int id)
        {
            var game = _gameService.Get(id);

            if (game == null)
                return NotFound();

            var comResources = _mapper.Map<IEnumerable<Comment>, 
                IEnumerable<CommentResource>>(game.Comments);
            return Ok(comResources);
        }

        [HttpPost, Route("{id}/comments")]
        public IHttpActionResult CreateComment([FromBody] CommentResource commentResource, int id)
        {
            if (commentResource == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var comment = _mapper.Map<CommentResource, Comment>(commentResource);
            comment.GameId = id;
            _commentService.Create(comment);

            return Ok(commentResource);
        }

        [HttpPost, Route("~/api/comments/{id}/comments")]
        public IHttpActionResult CreateAnswer([FromBody] CommentResource commentResource, int id)
        {
            if (commentResource == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var comment = _mapper.Map<CommentResource, Comment>(commentResource);
            comment.ParentCommentId = id;
            _commentService.Create(comment);

            return Ok(commentResource);
        }
    }
}
