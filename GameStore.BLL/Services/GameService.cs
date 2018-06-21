using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<PlatformType> _ptRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IRepository<Game> gameRepository, IUnitOfWork unitOfWork, 
            IRepository<PlatformType> ptRepository, IRepository<Genre> genreRepository)
        {
            _gameRepository = gameRepository;
            _ptRepository = ptRepository;
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateGame(Game game)
        {
            _gameRepository.Add(game);
            _unitOfWork.Complete();
        }

        public void DeleteGame(Game game)
        {
            _gameRepository.Remove(game);
            _unitOfWork.Complete();
        }

        public Game EditGame(int id, Game updatedGame)
        {
            var game = _gameRepository.Get(id);
            if (game == null) throw new ArgumentNullException();

            game.GameName = updatedGame.GameName;
            game.Description = updatedGame.Description;

            _unitOfWork.Complete();

            return game;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _gameRepository.GetAll();
        }

        public Game GetGame(int id)
        {
            return _gameRepository.Get(id);
        }

        public IEnumerable<Game> GetGamesByGenre(string name)
        {
            var genre = _genreRepository.Find(g => g.GenreName == name).FirstOrDefault();

            return _gameRepository.Find(g => g.Genres.Contains(genre));
        }

        public IEnumerable<Game> GetGamesByPlatformTypes(IEnumerable<PlatformType> platformTypes)
        {
            throw new NotImplementedException();
        }
    }
}
