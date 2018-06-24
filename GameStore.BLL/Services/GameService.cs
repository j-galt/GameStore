using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GameStore;
using GameStore.BLL.Utilities;
using System.Linq.Expressions;

namespace GameStore.BLL.Services
{
    public class GameService : Service<Game>, IGameService
    {
        private readonly IPlatformTypeRepository _ptRepository;
        private readonly IGenreRepository _genreRepository;
        
        public GameService(IRepository<Game> gameRepository, IUnitOfWork unitOfWork,
            IPlatformTypeRepository ptRepository, IGenreRepository genreRepository) 
            : base(gameRepository, unitOfWork)
        {
            _ptRepository = ptRepository;
            _genreRepository = genreRepository;
        }

        public Game Get(int id)
        {
            return base.Get(g => g.GameId == id,
                g => g.Genres, g => g.Publisher,
                g => g.PlatformTypes, g => g.Comments);
        }

        public override Game Edit(int id, Game updatedGame)
        {            
            var game = _repository.GetWithIncludes(g => g.GameId == id, 
                g => g.Genres, g => g.PlatformTypes)
                .FirstOrDefault();

            if (game == null) throw new ArgumentNullException();

            game.GameName = updatedGame.GameName;
            game.Description = updatedGame.Description;

            var updatedGenres = _genreRepository
                .GetGenresByNames(updatedGame.Genres
                .Select(g => g.GenreName));

            var updatedPt = _ptRepository
                .GetPlatformTypesByNames(updatedGame.PlatformTypes
                .Select(pt => pt.Type));

            game.Genres.RefreshItems(updatedGenres.ToList());
            game.PlatformTypes.RefreshItems(updatedPt.ToList());
           
            _unitOfWork.Complete();

            return game;
        }     

        public IEnumerable<Game> GetGamesByGenre(string name)
        {
            var genre = _genreRepository
                .Find(g => g.GenreName == name)
                .FirstOrDefault();

            return _repository.Find(g => g.Genres.Contains(genre));
        }

        public IEnumerable<Game> GetGamesByPlatformTypes(IEnumerable<PlatformType> platformTypes)
        {
            throw new NotImplementedException();
        }


    }   
}
