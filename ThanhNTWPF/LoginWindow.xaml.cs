using Services;
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
using System.Windows.Shapes;

namespace ThanhNTWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ICustomerService iCustomerService;

        public LoginWindow()
        {
            InitializeComponent();
            iCustomerService = new CustomerService();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var temp = iCustomerService.CheckLogin(TxtEmail.Text, TxtPassword.Password);
            if (temp != null)
            {
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("You are not permission!");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
