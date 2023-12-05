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
        public PlantWindow()
        {
            InitializeComponent();
            UpdateUI();
        }

        private async void UpdateUI()
        {
            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                var allPlants = await uow.PlantRepository.GetAllWithInstructions();

                foreach (var plant in allPlants)
                {
                    ListViewItem item = new();
                    item.Tag = plant;
                    item.Content = plant.CommonName;
                    lstPlants.Items.Add(item);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyGardenWindow myGardenWindow = new();
            myGardenWindow.Show();
            Close();
        }

        private void lstPlants_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListViewItem selectedItem = (ListViewItem)lstPlants.SelectedItem;
            PlantModel selectedPlant = (PlantModel)selectedItem.Tag;

            PlantManager.SelectedPlant = selectedPlant;

            //Skickar med Id och varifrån jag kommer så jag vet vart jag ska gå om jag klicka "back"
            PlantDetailsWindow detailsWindow = new(selectedPlant.PlantId, "PlantWindow");
            detailsWindow.Show();
            Close();
        }
    }
}
