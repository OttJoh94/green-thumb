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

                //Returnerar null om det inte går att hämta användaren
                var user = await uow.UserRepository.GetUser(username, password);

                if (user != null)
                {
                    UserManager.SignedInUser = user;

                    //Går till olika fönster om det är första gången man loggar in och inte har någon Garden än
                    if (user.GardenId == null)
                    {
                        //Gå till GardenWindow
                        MyGardenWindow myGardenWindow = new("First time signing in");
                        myGardenWindow.Show();
                        Close();
                    }
                    else
                    {
                        //Gå till PlantWindow
                        MyGardenWindow plantWindow = new();
                        plantWindow.Show();
                        Close();
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
