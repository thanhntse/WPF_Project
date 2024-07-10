using BusinessObjects.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThanhNTWPF.Views.Admin.Control
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : UserControl
    {
        private readonly ICustomerService CustomerService;
        public List<Customer> Customers { get; set; } = null!;

        public List<Customer> FilterCustomers { get; set; } = null!;

        public UserManagement()
        {
            InitializeComponent();
            CustomerService = new CustomerService();
            LoadCustomerList();
            this.DataContext = this;
        }

        private void ResetInput()
        {
            TxtFullName.Text = "";
            TxtTelephone.Text = "";
            TxtEmail.Text = "";
            DateBirthday.SelectedDate = null;
            RbActive.IsChecked = false;
            RbInactive.IsChecked = false;
            TxtPassword.Password = "";
        }

        public void LoadCustomerList()
        {
            try
            {
                Customers = CustomerService.GetAll();
                FilterCustomers = CustomerService.GetAll();
                DgData.ItemsSource = FilterCustomers;
            }
            catch
            {
                MessageBox.Show("Error on load customer list");
            }
            finally
            {
                ResetInput();
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgData.SelectedItem != null && DgData.SelectedItem is Customer selectedCustomer)
            {
                TxtFullName.Text = selectedCustomer.CustomerFullName;
                TxtTelephone.Text = selectedCustomer.Telephone;
                TxtEmail.Text = selectedCustomer.EmailAddress;

                DateBirthday.SelectedDate = selectedCustomer.CustomerBirthday;

                if (selectedCustomer.CustomerStatus == 1)
                {
                    RbActive.IsChecked = true;
                    RbInactive.IsChecked = false;
                }
                else
                {
                    RbActive.IsChecked = false;
                    RbInactive.IsChecked = true;
                }

                TxtPassword.Password = selectedCustomer.Password;
            }
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetInput();
            DgData.SelectedItem = null;
            UserDialogPopup.IsOpen = true;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DgData.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer to update.");
            }
            else UserDialogPopup.IsOpen = true;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgData.SelectedItem != null && DgData.SelectedItem is Customer selectedCustomer)
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete customer with ID {selectedCustomer.CustomerId}?"
                        , "Confirmation", MessageBoxButton.YesNo
                        , MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        CustomerService.DeleteCustomer(selectedCustomer);
                        LoadCustomerList();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ResetInput();
                DgData.SelectedItem = null;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgData.SelectedItem != null && DgData.SelectedItem is Customer selectedCustomer)
                {
                    selectedCustomer.CustomerFullName = TxtFullName.Text;
                    selectedCustomer.Telephone = TxtTelephone.Text;
                    selectedCustomer.EmailAddress = TxtEmail.Text;
                    selectedCustomer.CustomerBirthday = DateBirthday.SelectedDate;
                    selectedCustomer.CustomerStatus = RbActive.IsChecked == true ? (byte)1 : (byte)0;
                    selectedCustomer.Password = TxtPassword.Password;

                    CustomerService.UpdateCustomer(selectedCustomer);
                }
                else
                {
                    Customer customer = new()
                    {
                        CustomerFullName = TxtFullName.Text,
                        Telephone = TxtTelephone.Text,
                        EmailAddress = TxtEmail.Text,
                        CustomerBirthday = DateBirthday.SelectedDate,
                        CustomerStatus = (byte?)(RbActive.IsChecked == true ? 1 : 0),
                        Password = TxtPassword.Password
                    };
                    CustomerService.SaveCustomer(customer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                UserDialogPopup.IsOpen = false;
                LoadCustomerList();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDialogPopup.IsOpen = false;
            ResetInput();
            DgData.SelectedItem = null;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterCustomers = Customers.Where(c =>
                (string.IsNullOrEmpty(TxtSearchName.Text) || c.CustomerFullName.ToUpper().Contains(TxtSearchName.Text.ToUpper())) &&
                (string.IsNullOrEmpty(TxtSearchPhone.Text) || c.Telephone.ToUpper().Contains(TxtSearchPhone.Text.ToUpper())) &&
                (string.IsNullOrEmpty(TxtSearchEmail.Text) || c.EmailAddress.ToUpper().Contains(TxtSearchEmail.Text.ToUpper()))
            ).ToList();

            DgData.ItemsSource = FilterCustomers;
        }

    }
}
