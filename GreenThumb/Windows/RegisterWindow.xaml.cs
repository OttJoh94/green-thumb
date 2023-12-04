using GreenThumb.Database;
using GreenThumb.Models;
using System.Windows;

namespace GreenThumb.Windows
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = pwPassword.Password.ToString();

            if (username == "" || password == "") return;

            using (GreenDbContext context = new())
            {
                GreenUnitOfWork uow = new(context);

                //Kontrollerar om användarnamnet är upptaget
                bool usernameTaken = await uow.UserRepository.UsernameTaken(username);

                if (usernameTaken)
                {
                    MessageBox.Show("Username is already taken. Try another", "Error");
                    return;
                }

                UserModel newUser = new() { Username = username, Password = password };

                await uow.UserRepository.AddAsync(newUser);
                await uow.CompleteAsync();

                MessageBox.Show($"Welcome {username}! \nYou can now log in", "Success");

                SignInWindow signInWindow = new();
                signInWindow.Show();
                Close();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = new();
            signInWindow.Show();
            Close();
        }
    }
}
