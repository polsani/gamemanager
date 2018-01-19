using GameManager.Domain.Contracts.Helpers;
using GameManager.Domain.Contracts.Services;
using GameManager.Domain.Entities;
using GameManager.Domain.Utils;

namespace GameManager.Application.Services
{
    public class DunService : IDunService
    {
        private readonly IEmailHelper _emailHelper;
        private readonly ILoanService _loanService;

        public DunService(ILoanService loanService, IEmailHelper emailHelper)
        {
            _loanService = loanService;
            _emailHelper = emailHelper;
        }

        public void AskLateLoansForReturn()
        {
            var lateLoans = _loanService.GetLateLoans();

            lateLoans.ForEach(x => AskForReturn(x));
        }

        private void AskForReturn(Loan loan)
        {
            loan.AlreadyAskedForReturn = true;
            _loanService.Update(loan);

            SendEmailToFriend(loan);
        }        

        private void SendEmailToFriend(Loan loan)
        {
            const string SUBJECT = "Devolução de jogo";
            string MESSAGE = string.Format("Olá {0}, gostaria de lembrar que o meu jogo {1} está com você e estou precisando dele. Obrigado", 
                loan.Friend.Name, loan.BorrowedGame.Title);

            var email = new Email
            {
                Message = MESSAGE,
                To = loan.Friend.Email,
                Subject = SUBJECT
            };

            _emailHelper.SendAsync(email);
        }
    }
}
