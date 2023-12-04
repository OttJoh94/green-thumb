using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    internal class GardenModel
    {
        [Key]
        [Column("id")]
        public int GardenId { get; set; }
        [Column("square_meters")]
        public int SquareMeters { get; set; }
        [Column("location")]
        public string Location { get; set; } = null!;
        [Column("environment")]
        public string? Environment { get; set; }
        public List<GardenPlantModel> GardenPlants { get; set; } = new();
        public UserModel? User { get; set; }

    }
}
