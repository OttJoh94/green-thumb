using GreenThumb.Models;

namespace GreenThumb.Database
{
    internal class GardenRepository(GreenDbContext context) : Repository<GardenModel>(context)
    {
        private readonly GreenDbContext _context = context;

        public async Task UpdateGarden(int gardenId, GardenModel newGarden)
        {
            var gardenToUpdate = await GetByIdAsync(gardenId);

            if (gardenToUpdate != null)
            {
                gardenToUpdate.SquareMeters = newGarden.SquareMeters;
                gardenToUpdate.Location = newGarden.Location;
                gardenToUpdate.Environment = newGarden.Environment;
            }
        }
    }
}
