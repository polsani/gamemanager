using GameManager.Data.Context;
using GameManager.Data.Repositories;
using GameManager.Domain.Contracts.Data;
using GameManager.Domain.Contracts.Data.Repositories;
using GameManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GameManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DefaultContext _context;

        private IGenericRepository<Friend> _friendRepository;
        private IGenericRepository<Game> _gameRepository;
        private IGenericRepository<Loan> _loanRepository;

        public IGenericRepository<Friend> FriendRepository
        {
            get
            {
                if (_friendRepository == null)
                    _friendRepository = new GenericRepository<Friend>(_context);

                return _friendRepository;
            }
        }
        public IGenericRepository<Game> GameRepository
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new GenericRepository<Game>(_context);

                return _gameRepository;
            }
        }
        public IGenericRepository<Loan> LoanRepository
        {
            get
            {
                if (_loanRepository == null)
                    _loanRepository = new GenericRepository<Loan>(_context);

                return _loanRepository;
            }
        }

        public UnitOfWork(IConfigurationRoot configuration)
        {
            var optionBuilder = new DbContextOptionsBuilder<DefaultContext>();

            optionBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            _context = new DefaultContext(optionBuilder.Options);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
