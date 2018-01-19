using GameManager.Domain.Contracts.Data.Repositories;
using GameManager.Domain.Entities;

namespace GameManager.Domain.Contracts.Data
{
    public interface IUnitOfWork
    {
        IGenericRepository<Friend> FriendRepository { get; }
        IGenericRepository<Game> GameRepository { get; }
        IGenericRepository<Loan> LoanRepository { get; }
        void Save();
        void Dispose();
    }
}
