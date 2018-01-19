using GameManager.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameManager.Web.Models
{
    public class FriendViewModel
    {
        [Display(Name = "Código")]
        public string Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public virtual ICollection<LoanViewModel> Loans { get; set; }

        public static ICollection<FriendViewModel> ConvertCollectionFromDomain(IEnumerable<Friend> friends)
        {
            return friends.Select(x => new FriendViewModel
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.Name,
                Phone = x.Phone
            }).ToList();
        }

        public static FriendViewModel ConvertFromDomain(Friend friend)
        {
            return new FriendViewModel {
                Id = friend.Id,
                Email = friend.Email,
                Name = friend.Name,
                Phone = friend.Phone
            };
        }

        public Friend ConvertToDomain()
        {
            return new Friend
            {
                Email = Email,
                Id = Id,
                Name = Name,
                Phone = Phone
            };
        }
    }
}
