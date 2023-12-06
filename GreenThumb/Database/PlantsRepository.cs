using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    internal class PlantsRepository(GreenDbContext context) : Repository<PlantModel>(context)
    {
        private readonly GreenDbContext _context = context;

        //Hämtar alla gardenplants inkluderat med plant
        public async Task<List<GardenPlantModel>> GetGardenPlantsIncludingPlant(int gardenId)
        {
            var myGardenPlants = await _context.GardenPlants.Include(g => g.Plant).Where(g => g.GardenId == gardenId).ToListAsync();

            return myGardenPlants;
        }

        public async Task UpdatePlantAsync(int id, PlantModel newPlant)
        {
            PlantModel? plantToUpdate = await GetByIdAsync(id);

            if (plantToUpdate != null)
            {
                plantToUpdate.CommonName = newPlant.CommonName;
                plantToUpdate.ScientificName = newPlant.ScientificName;
            }
        }

        public async Task<bool> PlantAlreadyAdded(string newPlant)
        {
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.CommonName == newPlant);

            if (plant == null)
            {
                return false;
            }
            return true;
        }

    }
}
