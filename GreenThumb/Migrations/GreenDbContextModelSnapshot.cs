﻿// <auto-generated />
using GreenThumb.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreenThumb.Migrations
{
    [DbContext(typeof(GreenDbContext))]
    partial class GreenDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GreenThumb.Models.GardenModel", b =>
                {
                    b.Property<int>("GardenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GardenId"), 1L, 1);

                    b.Property<string>("Environment")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("environment");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("location");

                    b.Property<int>("SquareMeters")
                        .HasColumnType("int")
                        .HasColumnName("square_meters");

                    b.HasKey("GardenId");

                    b.ToTable("Gardens");

                    b.HasData(
                        new
                        {
                            GardenId = 1,
                            Environment = "Greenhouse",
                            Location = "Sweden",
                            SquareMeters = 120
                        },
                        new
                        {
                            GardenId = 2,
                            Environment = "Field",
                            Location = "England",
                            SquareMeters = 6400
                        });
                });

            modelBuilder.Entity("GreenThumb.Models.GardenPlantModel", b =>
                {
                    b.Property<int>("PlantId")
                        .HasColumnType("int")
                        .HasColumnName("plant_id");

                    b.Property<int>("GardenId")
                        .HasColumnType("int")
                        .HasColumnName("garden_id");

                    b.HasKey("PlantId", "GardenId");

                    b.HasIndex("GardenId");

                    b.ToTable("GardenPlants");

                    b.HasData(
                        new
                        {
                            PlantId = 1,
                            GardenId = 1
                        },
                        new
                        {
                            PlantId = 2,
                            GardenId = 1
                        },
                        new
                        {
                            PlantId = 4,
                            GardenId = 1
                        },
                        new
                        {
                            PlantId = 7,
                            GardenId = 1
                        });
                });

            modelBuilder.Entity("GreenThumb.Models.InstructionModel", b =>
                {
                    b.Property<int>("InstructionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructionId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("PlantId")
                        .HasColumnType("int")
                        .HasColumnName("plant_id");

                    b.HasKey("InstructionId");

                    b.HasIndex("PlantId");

                    b.ToTable("Instructions");

                    b.HasData(
                        new
                        {
                            InstructionId = 1,
                            Description = "Provide flowers with plenty of daily sunlight",
                            PlantId = 1
                        },
                        new
                        {
                            InstructionId = 2,
                            Description = "If growing sunflowers in a container, provide enough drainage and loose soil.",
                            PlantId = 1
                        },
                        new
                        {
                            InstructionId = 3,
                            Description = "Require a moderate amounts of water",
                            PlantId = 2
                        },
                        new
                        {
                            InstructionId = 4,
                            Description = "Watering infrequently.",
                            PlantId = 3
                        },
                        new
                        {
                            InstructionId = 5,
                            Description = "Prefer bright indirect light but no direct sun as the foliage will burn",
                            PlantId = 4
                        },
                        new
                        {
                            InstructionId = 6,
                            Description = "Let the top 25-50% of soil dry before watering.",
                            PlantId = 4
                        },
                        new
                        {
                            InstructionId = 7,
                            Description = "Peonies need a well-drained position, and are fine with most soil types as long as it is not waterlogged",
                            PlantId = 5
                        },
                        new
                        {
                            InstructionId = 8,
                            Description = "They enjoy full sun, but can cope with a small amount of shade.",
                            PlantId = 5
                        },
                        new
                        {
                            InstructionId = 9,
                            Description = "Require water once a week",
                            PlantId = 6
                        },
                        new
                        {
                            InstructionId = 10,
                            Description = "Position your orchid in a bright windowsill facing east or west.",
                            PlantId = 6
                        },
                        new
                        {
                            InstructionId = 11,
                            Description = "Weekly feeding with a fertilizer designed for orchids.",
                            PlantId = 6
                        },
                        new
                        {
                            InstructionId = 12,
                            Description = "Most orchids require water once a week",
                            PlantId = 6
                        },
                        new
                        {
                            InstructionId = 13,
                            Description = "Cut off 1/2 inch from the bottom of the stem every day in the water",
                            PlantId = 7
                        },
                        new
                        {
                            InstructionId = 14,
                            Description = "Top off the water with cold water daily",
                            PlantId = 7
                        },
                        new
                        {
                            InstructionId = 15,
                            Description = "do not put the vase in direct sun",
                            PlantId = 7
                        });
                });

            modelBuilder.Entity("GreenThumb.Models.PlantModel", b =>
                {
                    b.Property<int>("PlantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlantId"), 1L, 1);

                    b.Property<string>("CommonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("common_name");

                    b.Property<string>("ScientificName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("scientific_name");

                    b.HasKey("PlantId");

                    b.ToTable("Plants");

                    b.HasData(
                        new
                        {
                            PlantId = 1,
                            CommonName = "Sunflower",
                            ScientificName = "Helianthus annuus"
                        },
                        new
                        {
                            PlantId = 2,
                            CommonName = "Rose",
                            ScientificName = "Rosa rubiginosa"
                        },
                        new
                        {
                            PlantId = 3,
                            CommonName = "Cactus",
                            ScientificName = "Cactaceae"
                        },
                        new
                        {
                            PlantId = 4,
                            CommonName = "Common ivy",
                            ScientificName = "Hedera helix"
                        },
                        new
                        {
                            PlantId = 5,
                            CommonName = "Peony",
                            ScientificName = "Paeoniaceae"
                        },
                        new
                        {
                            PlantId = 6,
                            CommonName = "Orchid",
                            ScientificName = "Phalaenopsis"
                        },
                        new
                        {
                            PlantId = 7,
                            CommonName = "Tulip",
                            ScientificName = "Tulipa gesneriana"
                        });
                });

            modelBuilder.Entity("GreenThumb.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("GardenId")
                        .HasColumnType("int")
                        .HasColumnName("garden_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.HasIndex("GardenId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            GardenId = 1,
                            Password = "password",
                            Username = "user"
                        });
                });

            modelBuilder.Entity("GreenThumb.Models.GardenPlantModel", b =>
                {
                    b.HasOne("GreenThumb.Models.GardenModel", "Garden")
                        .WithMany("GardenPlants")
                        .HasForeignKey("GardenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenThumb.Models.PlantModel", "Plant")
                        .WithMany("GardenPlants")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garden");

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("GreenThumb.Models.InstructionModel", b =>
                {
                    b.HasOne("GreenThumb.Models.PlantModel", "Plant")
                        .WithMany("Instructions")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plant");
                });

            modelBuilder.Entity("GreenThumb.Models.UserModel", b =>
                {
                    b.HasOne("GreenThumb.Models.GardenModel", "Garden")
                        .WithOne("User")
                        .HasForeignKey("GreenThumb.Models.UserModel", "GardenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garden");
                });

            modelBuilder.Entity("GreenThumb.Models.GardenModel", b =>
                {
                    b.Navigation("GardenPlants");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GreenThumb.Models.PlantModel", b =>
                {
                    b.Navigation("GardenPlants");

                    b.Navigation("Instructions");
                });
#pragma warning restore 612, 618
        }
    }
}
