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
        public List<BookingReservation> BookingReservations { get; set; }

        public History(Customer cus)
        {
            InitializeComponent();
            BookingReservationService = new BookingReservationService();
            this.BookingReservations = BookingReservationService.GetBookingReservationsOfCustomer(cus.CustomerId);
            this.DataContext = this;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
