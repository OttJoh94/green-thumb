using GreenThumb.Models;

namespace GreenThumb.Database
{
    internal class GreenUnitOfWork(GreenDbContext context)
    {
        private readonly GreenDbContext _context = context;
        public UserRepository UserRepository { get; } = new(context);
        public Repository<GardenModel> GardenRepository { get; } = new(context);
        public Repository<GardenPlantModel> GardenPlantRepository { get; } = new(context);
        public PlantsRepository PlantRepository { get; } = new(context);
        public Repository<InstructionModel> InstructionRepository { get; } = new(context);

        public async Task CompleteAsync() => await _context.SaveChangesAsync();
    }
}
