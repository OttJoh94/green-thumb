using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    internal class PlantsRepository : Repository<PlantModel>
    {
        private readonly GreenDbContext _context;

        public PlantsRepository(GreenDbContext context) : base(context)
        {
            _context = context;
        }

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
