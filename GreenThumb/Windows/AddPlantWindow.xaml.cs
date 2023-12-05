using GreenThumb.Database;
using GreenThumb.Models;
using System.Windows;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for AddPlantWindow.xaml
    /// </summary>
    public partial class AddPlantWindow : Window
    {
        List<string> _instructions = new();
        public AddPlantWindow()
        {
            InitializeComponent();
        }

        private void btnAddInstruction_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInstruction.Text)) return;

            string instruction = txtInstruction.Text;
            //Lägger bara in en sträng för det är det enda vi behöver
            _instructions.Add(instruction);

            UpdateInstructions();
        }

        private void UpdateInstructions()
        {
            lstInstructions.Items.Clear();
            txtInstruction.Text = "";
            foreach (string instruction in _instructions)
            {
                lstInstructions.Items.Add(instruction);
            }
        }

        private void btnRemoveInstruction_Click(object sender, RoutedEventArgs e)
        {
            if (lstInstructions.SelectedItem == null) return;

            string instruction = lstInstructions.SelectedItem.ToString();

            if (instruction != null)
            {
                _instructions.Remove(instruction);
            }

            UpdateInstructions();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PlantWindow plantWindow = new();
            plantWindow.Show();
            Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string commonName = txtCommonName.Text;
            string scientificName = txtScientificName.Text;

            if (string.IsNullOrWhiteSpace(commonName) || string.IsNullOrWhiteSpace(scientificName))
            {
                MessageBox.Show("Invalid names");
                return;
            }
            if (_instructions.Count <= 0)
            {
                MessageBox.Show("No instructions added");
                return;
            }

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                bool alreadyAdded = await uow.PlantRepository.PlantAlreadyAdded(commonName);

                if (alreadyAdded)
                {
                    MessageBox.Show("That plant is already added to the database");
                    return;
                }

                PlantModel newPlant = new() { CommonName = commonName, ScientificName = scientificName, };
                await uow.PlantRepository.AddAsync(newPlant);
                await uow.CompleteAsync();

                foreach (string instruction in _instructions)
                {
                    InstructionModel newInstruction = new() { Description = instruction, PlantId = newPlant.PlantId };
                    await uow.InstructionRepository.AddAsync(newInstruction);
                }
                await uow.CompleteAsync();

            }

            MessageBox.Show($"{commonName} - {scientificName} added to the database!", "Success!");
            PlantWindow plantWindow = new();
            plantWindow.Show();
            Close();

        }
    }
}
