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

namespace ThanhNTWPF.Views.Admin.Control
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        private readonly IBookingReservationService BookingReservationService;
        public Reports()
        {
            InitializeComponent();
            BookingReservationService = new BookingReservationService();
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.MaxValue;

            var reportData = BookingReservationService.GetByDateRange(startDate, endDate);
            ReportDataGrid.ItemsSource = reportData;
        }
    }
}
