using GreenThumb.Models;

namespace GreenThumb.Database
{
    internal class InstructionsRepository(GreenDbContext context) : Repository<InstructionModel>(context)
    {
        private readonly GreenDbContext _context = context;

        public async Task<List<InstructionModel>> GetAllInstructionsById(int id)
        {
            var allInstructions = await GetAllAsync();

            var filteredInstructions = allInstructions.Where(i => i.PlantId == id).ToList();

            return filteredInstructions;
        }
    }
}
