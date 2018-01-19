using GameManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GameManager.Domain.Contracts.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll();
        IEnumerable<Game> Get(Func<Game, Boolean> predicate);
        IEnumerable<Game> GetAvailableGames();
        Game Get(string id);
        void Save(Game game);
        IEnumerable<Game> GetByTitle(string title);
        void Delete(string id);
    }
}
