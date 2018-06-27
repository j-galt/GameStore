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
        private readonly IRepository<PlatformType> _ptRepository;
        private readonly IRepository<Genre> _genreRepository;
        
        public GameService(IRepository<Game> gameRepository, IUnitOfWork unitOfWork,
            IRepository<PlatformType> ptRepository, IRepository<Genre> genreRepository) 
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

            var genreNames = updatedGame.Genres.Select(ug => ug.GenreName);
            var updatedGenres = _genreRepository.Find(g => genreNames
            .Any(ug => ug.Contains(g.GenreName)));

            var ptNames = updatedGame.PlatformTypes.Select(pt => pt.Type);
            var updatedPt = _ptRepository.Find(pt => ptNames
            .Any(upt => upt.Contains(pt.Type)));

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

            if (genre == null) throw new ArgumentNullException();

            return _repository.Find(g => g.Genres.Contains(genre));
        }

        public IEnumerable<Game> GetGamesByPlatformTypes(IEnumerable<PlatformType> platformTypes)
        {
            var types = platformTypes.Select(pt => pt.Type).ToList();

            return _repository.GetWithIncludes(g => 
                g.PlatformTypes.Any(pt => types.Contains(pt.Type)));
        }

    }   
}
