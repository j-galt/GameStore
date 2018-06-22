using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IRepository<PlatformType> _ptRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IGameRepository gameRepository, IUnitOfWork unitOfWork,
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
            var game = _gameRepository.GetGameWithRelatedData(id);
            if (game == null) throw new ArgumentNullException();

            game.GameName = updatedGame.GameName;
            game.Description = updatedGame.Description;

            // fps, shooter -> fps delete

            //Debug.WriteLine("************");
            //foreach (var gr in game.Genres)
            //    Debug.WriteLine(gr.GenreName);
            //Debug.WriteLine("-----");


            var gn = game.Genres.FirstOrDefault(x => x.GenreName == updatedGame.GameName);
            game.Genres.Remove(gn);
            _unitOfWork.Complete();

            foreach (var gr in game.Genres)
                Debug.WriteLine(gr.GenreName);



            //foreach (var genre in updatedGame.Genres)
            //    if (!game.Genres.Contains(genre))
            //        game.Genres.Remove(genre);



            //_gameRepository.UpdateGame(game);
            _unitOfWork.Complete();

            return game;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _gameRepository.GetAll();
        }

        public Game GetGame(int id)
        {
            return _gameRepository.GetGameWithRelatedData(id);
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
