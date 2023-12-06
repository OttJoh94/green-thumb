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
        //Sparar instructioner som strängar i en lista för att skapa InstructionModel efter PlantModel och kunna tilldela rätt PlantId.
        List<string> _instructions = new();
        public AddPlantWindow()
        {
            InitializeComponent();
        }

        private void btnAddInstruction_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInstruction.Text)) return;

            string instruction = txtInstruction.Text;

            //Sparar instructionen i en field variable för att använda senare
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
            //Har valt att alla blommor måste ha minst en instruction
            if (_instructions.Count <= 0)
            {
                MessageBox.Show("No instructions added");
                return;
            }

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                //Kontrollerar så blomman inte redan finns i registret
                bool alreadyAdded = await uow.PlantRepository.PlantAlreadyAdded(commonName);

                if (alreadyAdded)
                {
                    MessageBox.Show("That plant is already added to the database");
                    return;
                }
                //Lägger till en ny blomma
                PlantModel newPlant = new() { CommonName = commonName, ScientificName = scientificName, };
                await uow.PlantRepository.AddAsync(newPlant);
                await uow.CompleteAsync();

                //Efter Complete har newPlant tilldelats ett plantId vilket gör att vi kan skapa InstructionModel efter våra instruciontssträngar i field variable listan
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
