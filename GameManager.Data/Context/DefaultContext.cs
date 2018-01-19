using GameManager.Data.Identity;
using GameManager.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameManager.Data.Context
{
    public class DefaultContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            BuildFriendModelCreating(builder);
            BuildGameModelCreating(builder);
            BuildLoanModelCreating(builder);
        }

        private void BuildFriendModelCreating(ModelBuilder builder)
        {
            builder.Entity<Friend>(f =>
            {
                f.HasKey(x => x.Id);

                f.Property(x => x.Id).HasMaxLength(64).IsRequired();
                f.Property(x => x.Name).HasMaxLength(128).IsRequired();
                f.Property(x => x.Phone).HasMaxLength(16).IsRequired();
                f.Property(x => x.Email).HasMaxLength(128).IsRequired();

                f.HasMany(x => x.Loans).WithOne();
            });
        }

        private void BuildGameModelCreating(ModelBuilder builder)
        {
            builder.Entity<Game>(g =>
            {
                g.HasKey(x => x.Id);

                g.Property(x => x.Id).HasMaxLength(64).IsRequired();
                g.Property(x => x.Title).HasMaxLength(128).IsRequired();
                g.Property(x => x.Borrowed).IsRequired();

                g.HasMany(x => x.Loans).WithOne();
            });
        }

        private void BuildLoanModelCreating(ModelBuilder builder)
        {
            builder.Entity<Loan>(l =>
            {
                l.HasKey(x => x.Id);

                l.Property(x => x.Id).HasMaxLength(64).IsRequired();
                l.Property(x => x.LendDate).IsRequired();
                l.Property(x => x.ReturnDate).IsRequired();
                l.Property(x => x.AlreadyAskedForReturn).IsRequired();

                l.HasOne(x => x.BorrowedGame)
                        .WithMany(x => x.Loans)
                        .HasForeignKey(x => x.GameId);

                l.HasOne(x => x.Friend)
                        .WithMany(x => x.Loans)
                        .HasForeignKey(x => x.FriendId);
            });
        }
    }
}
