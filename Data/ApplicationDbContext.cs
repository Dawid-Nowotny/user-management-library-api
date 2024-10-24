using library_api.Models;
using Microsoft.EntityFrameworkCore;

namespace library_api.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookRental> BookRentals { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany(u => u.BookRental)
				.WithOne(br => br.User)
				.HasForeignKey(br => br.UserId);

			modelBuilder.Entity<Book>()
				.HasMany(b => b.BookRental)
				.WithOne(br => br.Book)
				.HasForeignKey(br => br.BookId);

			modelBuilder.Entity<Book>()
				.HasIndex(b => b.ISBN)
				.IsUnique();

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Username)
				.IsUnique();
		}
	}
}