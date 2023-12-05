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

                UserModel newUser = new() { Username = "Otto", Password = "123", GardenId = 2 };

                await uow.UserRepository.AddAsync(newUser);
                await uow.CompleteAsync();


                //InstructionModel instruction = new InstructionModel() { Description = "testa att vattna den", PlantId = testPlant.PlantId };


            }
        }

        public async void TestIgen()
        {
            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                var allPlants = await uow.PlantRepository.GetAllAsync();

                var allUsers = await uow.UserRepository.GetAllAsync();


            }


        }
    }

}