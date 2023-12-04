using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    internal class UserRepository(GreenDbContext context) : Repository<UserModel>(context)
    {
        private readonly GreenDbContext _context = context;

        public async Task<bool> UsernameTaken(string newUsername)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == newUsername);

            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<UserModel?> SignInUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            return user;
        }
    }
}
