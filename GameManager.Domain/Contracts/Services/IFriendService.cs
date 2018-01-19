using GameManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GameManager.Domain.Contracts.Services
{
    public interface IFriendService
    {
        IEnumerable<Friend> GetAll();
        IEnumerable<Friend> Get(Func<Friend, Boolean> predicate);
        Friend Get(string id);
        void Save(Friend friend);
    }
}
