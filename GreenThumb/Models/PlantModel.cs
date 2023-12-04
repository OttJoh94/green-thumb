using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    internal class PlantModel
    {
        [Key]
        [Column("id")]
        public int PlantId { get; set; }
        [Column("common_name")]
        public string CommonName { get; set; } = null!;
        [Column("scientific_name")]
        public string ScientificName { get; set; } = null!;
        public List<InstructionModel> Instructions { get; set; } = new();
        public List<GardenPlantModel> GardenPlants { get; set; } = new();
    }
}
