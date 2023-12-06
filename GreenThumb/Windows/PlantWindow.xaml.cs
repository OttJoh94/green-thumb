using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for PlantWindow.xaml
    /// </summary>
    public partial class PlantWindow : Window
    {
        private List<PlantModel> _allPlants = new();
        public PlantWindow()
        {
            InitializeComponent();
            UpdateMyPlantsList();
            UpdateAllPlantsList();
        }

        private async void UpdateAllPlantsList()
        {
            lstAllPlants.Items.Clear();

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                _allPlants = await uow.PlantRepository.GetAllAsync();

                foreach (var plant in _allPlants)
                {
                    ListViewItem item = new();
                    item.Tag = plant;
                    item.Content = plant.CommonName;
                    lstAllPlants.Items.Add(item);
                }
            }
        }

        private async void UpdateMyPlantsList()
        {

            lstMyPlants.Items.Clear();

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                // Om GardenID inte skulle finnas blir är gardenId satt till 0. Görs främst för att göra om int? till int
                int gardenId = UserManager.SignedInUser.GardenId ?? 0;
                if (gardenId != 0)
                {
                    //Hämtar alla gardenplants som tillhör rätt gardenId, inkludarar Plant för att komma åt CommonName i foreach-loopen
                    var FilteredGardenPlants = await uow.PlantRepository.GetGardenPlantsIncludingPlant(gardenId);

                    foreach (var gardenPlant in FilteredGardenPlants)
                    {
                        ListViewItem item = new();
                        item.Tag = gardenPlant;
                        item.Content = gardenPlant.Plant.CommonName;
                        lstMyPlants.Items.Add(item);
                    }
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyGardenWindow myGardenWindow = new();
            myGardenWindow.Show();
            Close();
        }

        private void lstAllPlants_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem selectedItem = (ListViewItem)lstAllPlants.SelectedItem;
            PlantModel selectedPlant = (PlantModel)selectedItem.Tag;

            //Skickar med Id och varifrån jag kommer så jag vet vart jag ska gå om jag klicka "back"
            PlantDetailsWindow detailsWindow = new(selectedPlant.PlantId, "PlantWindow");
            detailsWindow.Show();
            Close();
        }

        private void btnAddPlant_Click(object sender, RoutedEventArgs e)
        {
            AddPlantWindow addPlantWindow = new();
            addPlantWindow.Show();
            Close();
        }

        private void lstMyPlants_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem selectedItem = (ListViewItem)lstMyPlants.SelectedItem;
            GardenPlantModel selectedGardenPlant = (GardenPlantModel)selectedItem.Tag;

            //I myPlants-listan är det GardenPlantModel i listan istället för PlantModel, men kommer ändå åt PlantId.
            int plantId = selectedGardenPlant.PlantId;

            //Skickar med Id och varifrån jag kommer så jag vet vart jag ska gå om jag klicka "back"
            PlantDetailsWindow detailsWindow = new(plantId, "PlantWindow");
            detailsWindow.Show();
            Close();
        }

        private void btnGoToGarden_Click(object sender, RoutedEventArgs e)
        {
            MyGardenWindow myGarden = new();
            myGarden.Show();
            Close();
        }

        private async void btnDeletePlant_Click(object sender, RoutedEventArgs e)
        {
            if (lstAllPlants.SelectedItem == null) return;

            ListViewItem selectedItem = (ListViewItem)lstAllPlants.SelectedItem;
            PlantModel selectedPlant = (PlantModel)selectedItem.Tag;

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                uow.PlantRepository.Remove(selectedPlant);
                await uow.CompleteAsync();
            }

            UpdateAllPlantsList();
            UpdateMyPlantsList();
        }

        private async void btnAddToGarden_Click(object sender, RoutedEventArgs e)
        {
            if (lstAllPlants.SelectedItem == null) return;

            ListViewItem selectedItem = (ListViewItem)lstAllPlants.SelectedItem;
            PlantModel selectedPlant = (PlantModel)selectedItem.Tag;

            //Hämtar det jag behöver för att göra en GardenPlantModel. ?? 0 är för att konvertera från int? till int
            int plantId = selectedPlant.PlantId;
            int gardenId = UserManager.SignedInUser.GardenId ?? 0;

            if (gardenId != 0)
            {
                //Slängs iväg ett error om den inte gick att lägga till, och då är det för att den redan finns
                try
                {
                    using (GreenDbContext context = new())
                    {
                        GreenUnitOfWork uow = new(context);

                        GardenPlantModel newGardenPlantModel = new() { GardenId = gardenId, PlantId = plantId };

                        await uow.GardenPlantRepository.AddAsync(newGardenPlantModel);
                        await uow.CompleteAsync();
                    }
                }
                catch
                {
                    MessageBox.Show("Plant already added");
                }

            }

            UpdateMyPlantsList();
        }

        private async void btnRemoveFromGarden_Click(object sender, RoutedEventArgs e)
        {
            if (lstMyPlants.SelectedItem == null) return;

            ListViewItem selectedItem = (ListViewItem)lstMyPlants.SelectedItem;
            GardenPlantModel selectedGardenPlantModel = (GardenPlantModel)selectedItem.Tag;

            //Try catch bara för att säkra upp det.
            try
            {
                using (GreenDbContext context = new())
                {
                    GreenUnitOfWork uow = new(context);

                    uow.GardenPlantRepository.Remove(selectedGardenPlantModel);
                    await uow.CompleteAsync();
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong");
            }

            UpdateMyPlantsList();

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
            MessageBox.Show("Double click a plant in the list to show details. \nWrite in the searchbar above all plants to search for a plant \nDate planted will be set to when you add the plant to your garden");
        }

        private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Metoden filtrerar AllPlants efter vad som står i sökrutan hela tiden. Vet inte hur långsam metodn är om det är väldigt många plantor då den tömmer och fyller på listan vid varje knapptryck

            lstAllPlants.Items.Clear();
            string input = txtSearch.Text;

            //Om sökrutan är tom - visa alla plantor
            if (input == "")
            {
                UpdateAllPlantsList();
            }
            else
            {
                // Få fram alla plantor som börjar på det som står i sökrutan.

                var filteredPlants = from plant in _allPlants
                                     where plant.CommonName.StartsWith(input, StringComparison.OrdinalIgnoreCase)
                                     select plant;

                // Printa dom i listan
                foreach (var plant in filteredPlants)
                {
                    ListViewItem item = new();
                    item.Tag = plant;
                    item.Content = plant.CommonName;
                    lstAllPlants.Items.Add(item);
                }
            }
        }
    }
}

