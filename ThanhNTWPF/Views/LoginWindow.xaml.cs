using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.ApplicationServices;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ThanhNTWPF.Views.Admin;
using ThanhNTWPF.Views.Cust;

namespace ThanhNTWPF.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ICustomerService CustomerService;

        public LoginWindow()
        {
            InitializeComponent();
            CustomerService = new CustomerService();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private (string Email, string Password) GetAdminCredentials()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return (configuration["AdminCredentials:Email"], configuration["AdminCredentials:Password"]);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = TxtEmail.Text;
            string password = TxtPassword.Password;
            var adminCredentials = GetAdminCredentials();
            if (email == adminCredentials.Email && password == adminCredentials.Password)
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Close();
            }
            else
            {
                var temp = CustomerService.CheckLogin(email, password);
                if (temp != null)
                {
                    CustomerWindow customerWindow = new CustomerWindow(temp);
                    customerWindow.Show();
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("You are not permission!");
                }
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
