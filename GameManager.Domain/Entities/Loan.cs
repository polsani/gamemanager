using System;

namespace GameManager.Domain.Entities
{
    public class Loan
    {
        public string Id { get; set; }
        public DateTime LendDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string GameId { get; set; }
        public string FriendId { get; set; }
        public bool AlreadyAskedForReturn { get; set; }

        public virtual Game BorrowedGame { get; set; }
        public virtual Friend Friend { get; set; }
    }
}
