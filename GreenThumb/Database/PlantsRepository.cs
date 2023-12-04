using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    internal class PlantsRepository(GreenDbContext context) : Repository<PlantModel>(context)
    {
        private readonly GreenDbContext _context = context;

        public async Task<List<PlantModel>> GetAllWithInstructions()
        {
            return await _context.Plants.Include(p => p.Instructions).ToListAsync();
        }

        public async Task<PlantModel?> GetPlantWithInstructions(int id)
        {
            return await _context.Plants.Include(p => p.Instructions).FirstOrDefaultAsync(p => p.PlantId == id);
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
    }
}
