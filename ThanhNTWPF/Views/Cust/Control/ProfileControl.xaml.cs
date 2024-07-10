using BusinessObjects.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ThanhNTWPF.Views.Cust.Control
{
    /// <summary>
    /// Interaction logic for ProfileControl.xaml
    /// </summary>
    public partial class ProfileControl : UserControl
    {
        private readonly ICustomerService CustomerService;
        public Customer Customer { get; set; }

        public ProfileControl(Customer cus)
        {
            InitializeComponent();
            CustomerService = new CustomerService();
            this.Customer = cus;
            this.DataContext = this;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer.CustomerFullName = TxtFullName.Text;
                Customer.Telephone = TxtTelephone.Text;
                Customer.EmailAddress = TxtEmail.Text;
                Customer.CustomerBirthday = DateBirthday.SelectedDate;
                Customer.Password = TxtPassword.Text;

                CustomerService.UpdateCustomer(Customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Update successfully!");
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this account?"
                    , "Confirmation", MessageBoxButton.YesNo
                    , MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    Customer.CustomerStatus = 0;
                    CustomerService.UpdateCustomer(Customer);
                    Window parentWindow = Window.GetWindow(this);
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    parentWindow?.Close();
                    MessageBox.Show("Delete successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
    }
}
