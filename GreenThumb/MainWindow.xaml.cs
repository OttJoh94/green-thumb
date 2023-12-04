using GreenThumb.Database;
using GreenThumb.Models;
using System.Windows;

namespace GreenThumb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestMethod();

        }

        private async Task TestMethod()
        {
            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                PlantModel testPlant = new() { CommonName = "Test", ScientificName = "Testus Blommus" };

                await uow.PlantRepository.UpdatePlantAsync(5, testPlant);
                await uow.CompleteAsync();


                //InstructionModel instruction = new InstructionModel() { Description = "testa att vattna den", PlantId = testPlant.PlantId };


            }
        }
    }
}