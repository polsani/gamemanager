using GameManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameManager.Web.Models
{
    public class LoanViewModel
    {
        [Display(Name = "Código")]
        public string Id { get; set; }

        [Display(Name = "Data empréstimo")]
        [DataType(DataType.Date)]
        public DateTime LendDate { get; set; }

        [Display(Name = "Data p/ retorno")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Data do retorno")]
        [DataType(DataType.Date)]
        public DateTime? ReturnedDate { get; set; }

        [Display(Name = "Jogo")]
        public string GameId { get; set; }

        [Display(Name = "Amigo")]
        public string FriendId { get; set; }

        [Display(Name = "Cobrou?")]
        public bool AlreadyAskedForReturn { get; set; }

        public virtual GameViewModel BorrowedGame { get; set; }
        public virtual FriendViewModel Friend { get; set; }

        public IEnumerable<FriendViewModel> Friends { get; set; }
        public IEnumerable<GameViewModel> Games { get; set; }

        public static ICollection<LoanViewModel> ConvertCollectionFromDomain(IEnumerable<Loan> loans)
        {
            return loans.Select(x => new LoanViewModel
            {
                Id = x.Id,
                AlreadyAskedForReturn = x.AlreadyAskedForReturn,
                LendDate = x.LendDate,
                ReturnDate = x.ReturnDate,
                ReturnedDate = x.ReturnedDate,
                BorrowedGame = GameViewModel.ConvertFromDomain(x.BorrowedGame),
                GameId = x.GameId,
                Friend = FriendViewModel.ConvertFromDomain(x.Friend),
                FriendId = x.FriendId
            }).ToList();
        }

        public static LoanViewModel ConvertFromDomain(Loan loan)
        {
            return new LoanViewModel
            {
                Id = loan.Id,
                AlreadyAskedForReturn = loan.AlreadyAskedForReturn,
                LendDate = loan.LendDate,
                ReturnDate = loan.ReturnDate,
                ReturnedDate = loan.ReturnedDate,
                BorrowedGame = GameViewModel.ConvertFromDomain(loan.BorrowedGame),
                GameId = loan.GameId,
                Friend = FriendViewModel.ConvertFromDomain(loan.Friend),
                FriendId = loan.FriendId
            };
        }

        public Loan ConvertToDomain()
        {
            return new Loan
            {
                Id = Id,
                AlreadyAskedForReturn = AlreadyAskedForReturn,
                LendDate = LendDate,
                ReturnedDate = ReturnedDate,
                ReturnDate = ReturnDate,
                GameId = GameId,
                FriendId = FriendId
            };
        }
    }
}
