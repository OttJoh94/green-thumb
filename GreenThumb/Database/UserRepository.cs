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

        public async Task<UserModel?> GetUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            return user;
        }

        public async Task<GardenModel?> GetGardenFromUser(UserModel user)
        {
            var garden = await _context.Gardens.FirstOrDefaultAsync(g => g.GardenId == user.GardenId);

            return garden;
        }

        public async Task UpdateGardenIdOnUser(UserModel user, int gardenId)
        {
            var userToUpdate = await GetByIdAsync(user.UserId);
            if (userToUpdate != null)
            {
                userToUpdate.GardenId = gardenId;
            }
        }
    }
}
