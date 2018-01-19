using GameManager.Domain.Entities;
using System.Collections.Generic;

namespace GameManager.Domain.Contracts.Services
{
    public interface ILoanService
    {
        void Lend(Loan loan);
        IEnumerable<Loan> GetAll();
        IEnumerable<Loan> GetActiveLoans();
        Loan Get(string id);
        void Return(Loan loan);
        IEnumerable<Loan> GetLateLoans();
        void Update(Loan loan);
        void Delete(string id);
    }
}
