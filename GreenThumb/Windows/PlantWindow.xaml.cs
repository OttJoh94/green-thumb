﻿using GreenThumb.Database;
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
            UpdateLists();
        }

        private async void UpdateLists()
        {
            lstAllPlants.Items.Clear();
            lstMyPlants.Items.Clear();

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                var allPlants = await uow.PlantRepository.GetAllAsync();

                foreach (var plant in allPlants)
                {
                    ListViewItem item = new();
                    item.Tag = plant;
                    item.Content = plant.CommonName;
                    lstAllPlants.Items.Add(item);
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

            PlantManager.SelectedPlant = selectedPlant;

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

            UpdateLists();
        }

        private async void btnAddToGarden_Click(object sender, RoutedEventArgs e)
        {
            if (lstAllPlants.SelectedItem == null) return;
            ListViewItem selectedItem = (ListViewItem)lstAllPlants.SelectedItem;
            PlantModel selectedPlant = (PlantModel)selectedItem.Tag;

            int plantId = selectedPlant.PlantId;
            int gardenId = UserManager.SignedInUser.GardenId ?? 0;

            if (gardenId != 0)
            {
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

            UpdateLists();
        }

        private async void btnRemoveFromGarden_Click(object sender, RoutedEventArgs e)
        {
            if (lstMyPlants.SelectedItem == null) return;

            ListViewItem selectedItem = (ListViewItem)lstMyPlants.SelectedItem;
            GardenPlantModel selectedGardenPlantModel = (GardenPlantModel)selectedItem.Tag;

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

            UpdateLists();

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

