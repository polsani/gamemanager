using System.Collections.Generic;

namespace GameManager.Domain.Entities
{
    public class Game
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool Borrowed { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
