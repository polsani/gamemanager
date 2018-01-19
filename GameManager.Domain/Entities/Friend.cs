using System.Collections.Generic;

namespace GameManager.Domain.Entities
{
    public class Friend
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
