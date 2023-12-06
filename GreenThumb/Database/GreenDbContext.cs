using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using GreenThumb.Managers;
using GreenThumb.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    internal class GreenDbContext : DbContext
    {
        private readonly IEncryptionProvider _provider;
        public GreenDbContext()
        {
            _provider = new GenerateEncryptionProvider(KeyManager.GetEncryptionKey());
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<GardenModel> Gardens { get; set; }
        public DbSet<PlantModel> Plants { get; set; }
        public DbSet<InstructionModel> Instructions { get; set; }
        public DbSet<GardenPlantModel> GardenPlants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GreenThumbDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GardenPlantModel>().HasKey(g => new { g.PlantId, g.GardenId });

            modelBuilder.UseEncryption(_provider);


            //Seeding Data
            modelBuilder.Entity<PlantModel>().HasData(
                new PlantModel { PlantId = 1, CommonName = "Sunflower", ScientificName = "Helianthus annuus" },
                new PlantModel { PlantId = 2, CommonName = "Rose", ScientificName = "Rosa rubiginosa" },
                new PlantModel { PlantId = 3, CommonName = "Cactus", ScientificName = "Cactaceae" },
                new PlantModel { PlantId = 4, CommonName = "Common ivy", ScientificName = "Hedera helix" },
                new PlantModel { PlantId = 5, CommonName = "Peony", ScientificName = "Paeoniaceae" },
                new PlantModel { PlantId = 6, CommonName = "Orchid", ScientificName = "Phalaenopsis" },
                new PlantModel { PlantId = 7, CommonName = "Tulip", ScientificName = "Tulipa gesneriana" }
                );

            modelBuilder.Entity<GardenModel>().HasData(
                new GardenModel { GardenId = 1, SquareMeters = 120, Location = "Sweden", Environment = "Greenhouse" },
                new GardenModel { GardenId = 2, SquareMeters = 6400, Location = "England", Environment = "Field" }
                );

            modelBuilder.Entity<InstructionModel>().HasData(
                new InstructionModel { InstructionId = 1, Description = "Provide flowers with plenty of daily sunlight", PlantId = 1 },
                new InstructionModel { InstructionId = 2, Description = "If growing sunflowers in a container, provide enough drainage and loose soil.", PlantId = 1 },
                new InstructionModel { InstructionId = 3, Description = "Require a moderate amounts of water", PlantId = 2 },
                new InstructionModel { InstructionId = 4, Description = "Watering infrequently.", PlantId = 3 },
                new InstructionModel { InstructionId = 5, Description = "Prefer bright indirect light but no direct sun as the foliage will burn", PlantId = 4 },
                new InstructionModel { InstructionId = 6, Description = "Let the top 25-50% of soil dry before watering.", PlantId = 4 },
                new InstructionModel { InstructionId = 7, Description = "Peonies need a well-drained position, and are fine with most soil types as long as it is not waterlogged", PlantId = 5 },
                new InstructionModel { InstructionId = 8, Description = "They enjoy full sun, but can cope with a small amount of shade.", PlantId = 5 },
                new InstructionModel { InstructionId = 9, Description = "Require water once a week", PlantId = 6 },
                new InstructionModel { InstructionId = 10, Description = "Position your orchid in a bright windowsill facing east or west.", PlantId = 6 },
                new InstructionModel { InstructionId = 11, Description = "Weekly feeding with a fertilizer designed for orchids.", PlantId = 6 },
                new InstructionModel { InstructionId = 12, Description = "Most orchids require water once a week", PlantId = 6 },
                new InstructionModel { InstructionId = 13, Description = "Cut off 1/2 inch from the bottom of the stem every day in the water", PlantId = 7 },
                new InstructionModel { InstructionId = 14, Description = "Top off the water with cold water daily", PlantId = 7 },
                new InstructionModel { InstructionId = 15, Description = "do not put the vase in direct sun", PlantId = 7 }
                );

            modelBuilder.Entity<GardenPlantModel>().HasData(
                new GardenPlantModel { GardenId = 1, PlantId = 1, DatePlanted = new DateTime(2023, 06, 11) },
                new GardenPlantModel { GardenId = 1, PlantId = 2, DatePlanted = new DateTime(2023, 06, 15) },
                new GardenPlantModel { GardenId = 1, PlantId = 4, DatePlanted = new DateTime(2023, 08, 20) },
                new GardenPlantModel { GardenId = 1, PlantId = 7, DatePlanted = new DateTime(2023, 08, 20) }
                );

            modelBuilder.Entity<UserModel>().HasData(
                new UserModel { UserId = 1, Username = "user", Password = "password", GardenId = 1 }
                );
        }
    }
}
