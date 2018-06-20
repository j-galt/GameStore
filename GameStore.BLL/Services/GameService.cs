using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IRepository<Game> gameRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void CreateGame(GameDto gameDto)
        {
            var game = _mapper.Map<GameDto, Game>(gameDto);

            _gameRepository.Add(game);
            _unitOfWork.Complete();
        }

        public void DeleteGame(GameDto gameDto)
        {
            var game = _mapper.Map<GameDto, Game>(gameDto);

            _gameRepository.Remove(game);
            _unitOfWork.Complete();
        }

        public GameDto EditGame(string id, GameDto gameDto)
        {
            var game = _gameRepository.Get(id);
            if (game == null) throw new ArgumentNullException();

            game.GameName = gameDto.GameName;
            game.Description = gameDto.Description;

            _unitOfWork.Complete();

            return _mapper.Map<Game, GameDto>(game);
        }

        public IEnumerable<GameDto> GetAllGames()
        {
            var games = _gameRepository.GetAll();
            return _mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(games);
        }

        public GameDto GetGame(string id)
        {
            var game = _gameRepository.Get(id);
            return _mapper.Map<Game, GameDto>(game);
        }
    }
}
