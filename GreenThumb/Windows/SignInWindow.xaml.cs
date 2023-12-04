using GreenThumb.Database;
using GreenThumb.Managers;
using System.Windows;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new();
            registerWindow.Show();
            Close();
        }

        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = pwPassword.Password.ToString();

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                var user = await uow.UserRepository.SignInUser(username, password);

                if (user != null)
                {
                    UserManager.SignedInUser = user;
                    if (user.GardenId == null)
                    {
                        //Gå till GardenWindow
                        MessageBox.Show("GardenWindow");
                    }
                    else
                    {
                        //Gå till PlantWindow
                        MessageBox.Show("PlantWindow");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Try again", "Failed");
                }
            }
        }
    }
}
