using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for MyGardenWindow.xaml
    /// </summary>
    public partial class MyGardenWindow : Window
    {
        public MyGardenWindow()
        {
            InitializeComponent();
            FillInInfo();
        }

        public MyGardenWindow(string firstTime)
        {
            InitializeComponent();
            MessageBox.Show("Welcome to your garden! Since it's the first time you logged in you have to save your secifiks to your garden.", "Welcom");
            btnBrowsePlants.IsEnabled = false;
            btnRemovePlant.IsEnabled = false;
        }
        private async void FillInInfo()
        {
            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);
                var garden = await uow.UserRepository.GetGardenFromUser(UserManager.SignedInUser!);

                if (garden != null)
                {
                    txtSquareMeters.Text = garden.SquareMeters.ToString();
                    txtLocation.Text = garden.Location;
                    txtEnvironment.Text = garden.Environment;
                }

                // Om GardenID inte skulle finnas blir är gardenId satt till 0
                int gardenId = UserManager.SignedInUser.GardenId ?? 0;
                if (gardenId != 0)
                {
                    //Hämtar alla gardenplants som tillhör rätt gardenId, inkludarar Plant för att komma åt CommonName senare
                    var FilteredGardenPlants = await uow.PlantRepository.GetGardenPlantsIncludingPlant(gardenId);

                    foreach (var gardenPlant in FilteredGardenPlants)
                    {
                        ListViewItem item = new();
                        item.Tag = gardenPlant.Plant;
                        item.Content = new { PlantName = gardenPlant.Plant.CommonName, DateSeeded = gardenPlant.DatePlanted.ToShortDateString() };
                        lstPlants.Items.Add(item);
                    }
                }



            }
        }

        private async void btnSpecifics_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtSquareMeters.Text, out int squareMeters);
            string location = txtLocation.Text;
            string environment = txtEnvironment.Text;

            if (squareMeters == 0 || location == "" || environment == "")
            {
                MessageBox.Show("Invalid inputs", "Error");
                return;
            }

            GardenModel newGarden = new() { Location = location, Environment = environment, SquareMeters = squareMeters };

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);
                await uow.GardenRepository.AddAsync(newGarden);
                await uow.CompleteAsync();

                await uow.UserRepository.UpdateGardenIdOnUser(UserManager.SignedInUser!, newGarden.GardenId);
                await uow.CompleteAsync();

            }

            MessageBox.Show("Specifics saved!");

            btnBrowsePlants.IsEnabled = true;
            btnRemovePlant.IsEnabled = true;

        }


        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem selectedItem = (ListViewItem)lstPlants.SelectedItem;
            PlantModel selectedPlant = (PlantModel)selectedItem.Tag;

            PlantDetailsWindow detailsWindow = new(selectedPlant.PlantId, "MyGarden");
            detailsWindow.Show();
            Close();
        }
    }
}
