using GreenThumb.Database;
using GreenThumb.Managers;
using GreenThumb.Models;
using System.Windows;
using System.Windows.Controls;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for PlantDetailsWindow.xaml
    /// </summary>
    public partial class PlantDetailsWindow : Window
    {
        private string _comingFrom;
        private int _plantId;
        public PlantDetailsWindow(int plantId, string comingFrom)
        {
            InitializeComponent();
            _comingFrom = comingFrom;
            _plantId = plantId;
            UpdateTextBoxes();
            UpdateInstructions();
        }

        private async void UpdateInstructions()
        {
            lstInstructions.Items.Clear();
            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                var filteredInstructions = await uow.InstructionRepository.GetAllInstructionsById(_plantId);

                foreach (var instruction in filteredInstructions)
                {
                    ListViewItem item = new();
                    item.Tag = instruction;
                    item.Content = instruction.Description;
                    lstInstructions.Items.Add(item);
                }

            }

        }

        private async void UpdateTextBoxes()
        {
            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                var plant = await uow.PlantRepository.GetByIdAsync(_plantId);

                if (plant != null)
                {
                    txtCommonName.Text = plant.CommonName;
                    txtScientificName.Text = plant.ScientificName;
                }
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            txtCommonName.IsEnabled = true;
            txtScientificName.IsEnabled = true;
            btnEdit.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string commonName = txtCommonName.Text;
            string scientificName = txtScientificName.Text;

            PlantModel newPlant = new() { CommonName = commonName, ScientificName = scientificName };

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                await uow.PlantRepository.UpdatePlantAsync(_plantId, newPlant);
                await uow.CompleteAsync();
            }

            txtCommonName.IsEnabled = false;
            txtScientificName.IsEnabled = false;
            btnEdit.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;

            MessageBox.Show("Edit saved");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PlantManager.SelectedPlant = null;

            if (_comingFrom == "MyGarden")
            {
                MyGardenWindow myGardenWindow = new();
                myGardenWindow.Show();
                Close();
            }
            else
            {
                PlantWindow plantWindow = new();
                plantWindow.Show();
                Close();
            }
        }

        private async void btnAddInstruction_Click(object sender, RoutedEventArgs e)
        {
            string description = txtInstruction.Text;

            InstructionModel newInstruction = new() { Description = description, PlantId = _plantId };

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                await uow.InstructionRepository.AddAsync(newInstruction);
                await uow.CompleteAsync();
            }

            UpdateInstructions();
        }

        private async void btnRemoveInstruction_Click(object sender, RoutedEventArgs e)
        {
            if (lstInstructions.SelectedItem == null) return;
            ListViewItem selectedItem = (ListViewItem)lstInstructions.SelectedItem;
            InstructionModel selectedInstruction = (InstructionModel)selectedItem.Tag;

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);
                uow.InstructionRepository.Remove(selectedInstruction);
                await uow.CompleteAsync();
            }

            UpdateInstructions();
        }
    }
}
