﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GreenThumb.Models
{
    internal class GardenPlantModel
    {
        [Column("garden_id")]
        public int GardenId { get; set; }
        public GardenModel Garden { get; set; } = null!;
        [Column("plant_id")]
        public int PlantId { get; set; }
        public PlantModel Plant { get; set; } = null!;
        [Column("date_planted")]
        public DateTime DatePlanted { get; set; } = DateTime.Now;
    }
}
