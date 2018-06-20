using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IRepository<Game> gameRepository, IUnitOfWork unitOfWork)
        {
            _gameRepository = gameRepository;
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
    }
}
