using BusinessObjects.Models;
using Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThanhNTWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICustomerService CustomerService;

        public MainWindow()
        {
            InitializeComponent();
            CustomerService = new CustomerService();
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
                var customerList = CustomerService.GetAll();
                DgData.ItemsSource = customerList;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomerList();
        }

        private void DgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgData.SelectedItem != null && DgData.SelectedItem is Customer selectedCustomer)
            {
                TxtFullName.Text = selectedCustomer.CustomerFullName;
                TxtTelephone.Text = selectedCustomer.Telephone;
                TxtEmail.Text = selectedCustomer.EmailAddress;

                DateBirthday.SelectedDate = selectedCustomer.CustomerBirthday.HasValue
                    ? new DateTime(selectedCustomer.CustomerBirthday.Value.Year,
                           selectedCustomer.CustomerBirthday.Value.Month,
                           selectedCustomer.CustomerBirthday.Value.Day)
                     : (DateTime?)null;

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

        //private void BtnCreate_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Customer customer = new()
        //        {
        //            CustomerFullName = TxtFullName.Text,
        //            Telephone = TxtTelephone.Text,
        //            EmailAddress = TxtEmail.Text,
        //            CustomerBirthday = DateBirthday.SelectedDate.HasValue ? DateOnly.FromDateTime(DateBirthday.SelectedDate.Value) : null,
        //            CustomerStatus = (byte?)(RbActive.IsChecked == true ? 1 : 0),
        //            Password = TxtPassword.Password
        //        };
        //        CustomerService.SaveCustomer(customer);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        LoadCustomerList();
        //    }
        //}

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (DgData.SelectedItem != null && DgData.SelectedItem is Customer selectedCustomer)
        //        {
        //            selectedCustomer.CustomerFullName = TxtFullName.Text;
        //            selectedCustomer.Telephone = TxtTelephone.Text;
        //            selectedCustomer.EmailAddress = TxtEmail.Text;
        //            selectedCustomer.CustomerBirthday = DateBirthday.SelectedDate is DateTime date ? DateOnly.FromDateTime(date) : null;
        //            selectedCustomer.CustomerStatus = RbActive.IsChecked == true ? (byte)1 : (byte)0;
        //            selectedCustomer.Password = TxtPassword.Password;

        //            CustomerService.UpdateCustomer(selectedCustomer);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please select a customer to update.");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        LoadCustomerList();
        //    }
        //}

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
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
                LoadCustomerList();
            }
        }
    }
}