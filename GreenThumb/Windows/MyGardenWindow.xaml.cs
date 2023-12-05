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
            lblWelcome.Content = UserManager.SignedInUser.Username + "'s Garden";
        }

        public MyGardenWindow(string firstTime)
        {
            InitializeComponent();
            MessageBox.Show("Welcome to your garden! Since it's the first time you logged in you have to save your secifics to your garden.", "Welcom");
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



            }
            await UpdateMyPlants();
        }

        private async Task UpdateMyPlants()
        {
            lstPlants.Items.Clear();

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                // Om GardenID inte skulle finnas blir är gardenId satt till 0
                int gardenId = UserManager.SignedInUser.GardenId ?? 0;
                if (gardenId != 0)
                {
                    //Hämtar alla gardenplants som tillhör rätt gardenId, inkludarar Plant för att komma åt CommonName senare
                    var FilteredGardenPlants = await uow.PlantRepository.GetGardenPlantsIncludingPlant(gardenId);

                    foreach (var gardenPlant in FilteredGardenPlants)
                    {
                        ListViewItem item = new();
                        item.Tag = gardenPlant;
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

            //Körs allra första gången om inte user har tilldelats en garden än
            if (UserManager.SignedInUser.GardenId == null)
            {
                using (GreenDbContext context = new())
                {
                    GreenUnitOfWork uow = new(context);
                    await uow.GardenRepository.AddAsync(newGarden);
                    await uow.CompleteAsync();

                    await uow.UserRepository.UpdateGardenIdOnUser(UserManager.SignedInUser!, newGarden.GardenId);
                    await uow.CompleteAsync();

                    //Utan den här får man inte rätt GardenId om man inte loggar ut och in igen.
                    UserManager.SignedInUser.GardenId = newGarden.GardenId;
                }
            }
            else
            {
                int gardenId = UserManager.SignedInUser.GardenId ?? 0;

                using (GreenDbContext context = new())
                {
                    GreenUnitOfWork uow = new(context);

                    await uow.GardenRepository.UpdateGarden(gardenId, newGarden);
                    await uow.CompleteAsync();
                }
            }
            MessageBox.Show("Specifics saved!");

            btnBrowsePlants.IsEnabled = true;
            btnRemovePlant.IsEnabled = true;

        }


        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem selectedItem = (ListViewItem)lstPlants.SelectedItem;
            GardenPlantModel selectedPlant = (GardenPlantModel)selectedItem.Tag;

            PlantDetailsWindow detailsWindow = new(selectedPlant.Plant.PlantId, "MyGarden");
            detailsWindow.Show();
            Close();
        }

        private void btnBrowsePlants_Click(object sender, RoutedEventArgs e)
        {
            PlantWindow window = new();
            window.Show();
            Close();
        }

        private async void btnRemovePlant_Click(object sender, RoutedEventArgs e)
        {
            if (lstPlants.SelectedItem == null) return;

            ListViewItem selectedItem = (ListViewItem)lstPlants.SelectedItem;
            GardenPlantModel selectedPlant = (GardenPlantModel)selectedItem.Tag;

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                uow.GardenPlantRepository.Remove(selectedPlant);
                await uow.CompleteAsync();
            }

            await UpdateMyPlants();
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            UserManager.SignedInUser = null;

            SignInWindow signInWindow = new();
            signInWindow.Show();
            Close();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Double click a plant in the list to show details");
        }
    }
}
