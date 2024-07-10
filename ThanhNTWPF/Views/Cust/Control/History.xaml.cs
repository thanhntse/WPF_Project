using BusinessObjects.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThanhNTWPF.Views.Cust.Control
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
        private readonly IBookingReservationService BookingReservationService;
        private readonly IBookingDetailService BookingDetailService;
        public List<BookingReservation> BookingReservations { get; set; }
        public List<BookingDetail> BookingDetails { get; set; } = null!;

        public History(Customer cus)
        {
            InitializeComponent();
            BookingReservationService = new BookingReservationService();
            BookingDetailService = new BookingDetailService();
            this.BookingReservations = BookingReservationService.GetBookingReservationsOfCustomer(cus.CustomerId);
            this.DataContext = this;
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int bookingReservationId = (int)button.Tag;
                ViewDetailPopUp.IsOpen = true;
                BookingDetails = BookingDetailService.GetAllByBookingId(bookingReservationId);
                DgDataDetail.ItemsSource = BookingDetails;
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewDetailPopUp.IsOpen = false;
        }
    }
}
