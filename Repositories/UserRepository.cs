using library_api.Data;
using library_api.Models;
using library_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace library_api.Repositories
{
	public class UserRepository : IUserRepository
	{
		public readonly ApplicationDbContext _context;
		public UserRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<User?> GetByEmailAsync(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
		public async Task<User?> GetByUsernameAsync(string username)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		}

		public async Task AddAsync(User user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task<User?> GetUserInfoAsync(string identifier)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Username == identifier || u.Email == identifier);
		}
	}
}
