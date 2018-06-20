using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameStore.Web.Controllers.Api
{
    public class GamesController : ApiController
    {
        private readonly IRepository<Game> _gameRepository;

        public GamesController(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public Game Get(int id)
        {
            return _gameRepository.Get(id);
        }

        public void Post([FromBody]string value)
        {
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
