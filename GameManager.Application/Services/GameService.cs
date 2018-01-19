using System;
using System.Collections.Generic;
using GameManager.Domain.Contracts.Data;
using GameManager.Domain.Contracts.Services;
using GameManager.Domain.Entities;

namespace GameManager.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(string id)
        {
            var gameToDelete = _unitOfWork.GameRepository.Get(id);
            _unitOfWork.GameRepository.Delete(gameToDelete);

            _unitOfWork.Save();
        }

        public IEnumerable<Game> Get(Func<Game, bool> predicate)
        {
            return _unitOfWork.GameRepository.Get(predicate);
        }

        public Game Get(string id)
        {
            return _unitOfWork.GameRepository.Get(id);
        }

        public IEnumerable<Game> GetAll()
        {
            return _unitOfWork.GameRepository.GetAll();
        }

        public IEnumerable<Game> GetAvailableGames()
        {
            return Get(x => x.Borrowed == false);
        }

        public IEnumerable<Game> GetByTitle(string title)
        {
            return string.IsNullOrEmpty(title) ? GetAll() : Get(x => x.Title.ToLower().Contains(title.ToLower()));
        }

        public void Save(Game game)
        {
            if (string.IsNullOrEmpty(game.Id))
            {
                game.Id = Guid.NewGuid().ToString();

                _unitOfWork.GameRepository.Add(game);
            }
            else
                _unitOfWork.GameRepository.Update(game);

            _unitOfWork.Save();
        }
    }
}
