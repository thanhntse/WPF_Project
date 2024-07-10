using BusinessObjects.Models;
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
using ThanhNTWPF.Views.Cust.Control;

namespace ThanhNTWPF.Views.Cust
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private Customer customer = null!;
        
        public CustomerWindow(Customer cus)
        {
            InitializeComponent();
            this.customer = cus;
            LV.SelectedIndex = 0;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LV.SelectedItem is ListViewItem selectedItem)
            {
                switch (selectedItem.Tag.ToString())
                {
                    case "Home":
                        ContentArea.Content = new HomeControl();
                        break;
                    case "Profile":
                        ContentArea.Content = new ProfileControl(customer);
                        break;
                    case "History":
                        ContentArea.Content = new History(customer.BookingReservations);
                        break;
                    case "SignOut":
                        LoginWindow login = new LoginWindow();
                        login.Show();
                        this.Close();
                        break;
                }
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
