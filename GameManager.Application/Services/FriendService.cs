using System;
using System.Collections.Generic;
using GameManager.Domain.Contracts.Data;
using GameManager.Domain.Contracts.Services;
using GameManager.Domain.Entities;

namespace GameManager.Application.Services
{
    public class FriendService : IFriendService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FriendService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Save(Friend friend)
        {
            if (string.IsNullOrEmpty(friend.Id))
            {
                friend.Id = Guid.NewGuid().ToString();

                _unitOfWork.FriendRepository.Add(friend);
            }
            else
                _unitOfWork.FriendRepository.Update(friend);

            _unitOfWork.Save();
        }

        public IEnumerable<Friend> Get(Func<Friend, bool> predicate)
        {
            return _unitOfWork.FriendRepository.Get(predicate);
        }

        public IEnumerable<Friend> GetAll()
        {
            return _unitOfWork.FriendRepository.GetAll();
        }


        public Friend Get(string id)
        {
            return _unitOfWork.FriendRepository.Get(id);
        }
    }
}
