using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CheckMate.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)) 
                {
                ErrorText.Text = "Введите логин или пароль";
                return;
            }

            string ConnectionString = ConfigurationManager.ConnectionStrings["CheckMateSQL"].ConnectionString;
            string query = "SELECT Role.Role FROM Employees JOIN Role ON Employees.role_id = Role.id WHERE Employees.Login = @Login AND Employees.Password = @Password";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", password);
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    string role = result.ToString();

                    if (role == "Администратор" || role == "Официант")
                    {
                        new Menu().Show();
                        Window.GetWindow(this).Close();
                    }
                }
                else
                {
                    ErrorText.Text = "Неверный логин или пароль";
                }
                
            }
        }
    }
}
