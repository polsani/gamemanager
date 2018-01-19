using GameManager.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameManager.Web.Models
{
    public class GameViewModel
    {
        [Display(Name = "Código")]
        public string Id { get; set; }

        [Display(Name = "Título")]
        public string Title { get; set; }

        [Display(Name = "Emprestado")]
        public bool Borrowed { get; set; }

        public virtual ICollection<LoanViewModel> Loans { get; set; }

        public static ICollection<GameViewModel> ConvertCollectionFromDomain(IEnumerable<Game> games)
        {
            return games.Select(x => new GameViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Borrowed = x.Borrowed,
            }).ToList();
        }

        public static GameViewModel ConvertFromDomain(Game game)
        {
            return new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Borrowed = game.Borrowed
            };
        }

        public Game ConvertToDomain()
        {
            return new Game
            {
                Id = Id,
                Title = Title,
                Borrowed = Borrowed
            };
        }
    }
}
