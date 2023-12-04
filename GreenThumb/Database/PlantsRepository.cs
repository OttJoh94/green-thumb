using GreenThumb.Models;

namespace GreenThumb.Database
{
    internal class PlantsRepository : Repository<PlantModel>
    {
        private readonly GreenDbContext _context;

        public PlantsRepository(GreenDbContext context) : base(context)
        {
            _context = context;
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
