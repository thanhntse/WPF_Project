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

namespace ThanhNTWPF.Views.Admin.Control
{
    /// <summary>
    /// Interaction logic for BookingManagement.xaml
    /// </summary>
    public partial class BookingManagement : UserControl
    {
        private readonly IBookingReservationService BookingReservationService;
        private readonly ICustomerService CustomerService;
        private readonly IBookingDetailService BookingDetailService;
        private readonly IRoomInformationService RoomInformationService;
        public List<BookingReservation> BookingReservations { get; set; } = null!;
        public List<BookingReservation> FilteredBookings { get; set; } = null!;
        public List<Customer> Customers { get; set; } = null!;
        public List<RoomInformation> RoomInformations { get; set; } = null!;
        public List<BookingDetail> BookingDetails { get; set; } = null!;
        public int bookingId { get; set; }
        public BookingManagement()
        {
            InitializeComponent();
            BookingReservationService = new BookingReservationService();
            CustomerService = new CustomerService();
            BookingDetailService = new BookingDetailService();
            RoomInformationService = new RoomInformationService();
            LoadBookingList();
            Customers = CustomerService.GetAll();
            RoomInformations = RoomInformationService.GetAll();
            this.DataContext = this;
        }

        private void ResetInput()
        {
            DateBookingDate.SelectedDate = null;
            ComboCustomer.SelectedIndex = -1;
            RbActive.IsChecked = false;
            RbInactive.IsChecked = false;
        }

        private void ResetDetailInput()
        {
            StartDate.SelectedDate = null;
            EndDate.SelectedDate = null;
            ComboRoom.SelectedIndex = -1;
        }

        public void LoadBookingList()
        {
            try
            {
                BookingReservations = BookingReservationService.GetAll();
                BookingReservations.ForEach(
                    r =>
                    {
                        r.Customer = CustomerService.GetCustomerById(r.CustomerId);
                    }
                );
                FilteredBookings = BookingReservations;
                DgData.ItemsSource = FilteredBookings;
            }
            catch
            {
                MessageBox.Show("Error on load room list");
            }
            finally
            {
                ResetInput();
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgData.SelectedItem != null && DgData.SelectedItem is BookingReservation selectedBooking)
            {
                DateBookingDate.SelectedDate = selectedBooking.BookingDate;
                ComboCustomer.SelectedValue = selectedBooking.CustomerId;
                if (selectedBooking.BookingStatus == 1)
                {
                    RbActive.IsChecked = true;
                    RbInactive.IsChecked = false;
                }
                else
                {
                    RbActive.IsChecked = false;
                    RbInactive.IsChecked = true;
                }
            }
        }

        private void DataDetailGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgDataDetail.SelectedItem != null && DgDataDetail.SelectedItem is BookingDetail selectedBookingDetail)
            {
                StartDate.SelectedDate = selectedBookingDetail.StartDate;
                EndDate.SelectedDate = selectedBookingDetail.EndDate;
                ComboRoom.SelectedValue = selectedBookingDetail.RoomId;
            }
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetInput();
            DgData.SelectedItem = null;
            BookingDialogPopup.IsOpen = true;
        }

        private void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DgData.SelectedItem != null && DgData.SelectedItem is BookingReservation selectedBooking)
            {
                bookingId = selectedBooking.BookingReservationId;
                BookingDetails = BookingDetailService.GetAllByBookingId(bookingId);
                DgDataDetail.ItemsSource = BookingDetails;
                ViewDetailPopUp.IsOpen = true;
            }
            else
            {
                MessageBox.Show("Please select a booking to view.");
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DgData.SelectedItem == null)
            {
                MessageBox.Show("Please select a booking to update.");
            }
            else BookingDialogPopup.IsOpen = true;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgData.SelectedItem != null && DgData.SelectedItem is BookingReservation selectedBooking)
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete booking with ID {selectedBooking.BookingReservationId}?",
                        "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        List<BookingDetail> bookingDetails = BookingDetailService.GetAllByBookingId(selectedBooking.BookingReservationId);
                        bookingDetails.ForEach(
                            book => BookingDetailService.DeleteBookingDetail(book)
                        );
                        BookingReservationService.DeleteBookingReservation(selectedBooking);
                        LoadBookingList();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a booking to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DgData.SelectedItem = null;
                ResetInput();
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgData.SelectedItem != null && DgData.SelectedItem is BookingReservation selectedBooking)
                {
                    selectedBooking.BookingDate = DateBookingDate.SelectedDate;
                    selectedBooking.CustomerId = (int)ComboCustomer.SelectedValue;
                    selectedBooking.BookingStatus = RbActive.IsChecked == true ? (byte)1 : (byte)0;

                    BookingReservationService.UpdateBookingReservation(selectedBooking);
                }
                else
                {
                    BookingReservation booking = new BookingReservation
                    {
                        BookingDate = DateBookingDate.SelectedDate,
                        CustomerId = (int)ComboCustomer.SelectedValue,
                        BookingStatus = RbActive.IsChecked == true ? (byte)1 : (byte)0,
                        TotalPrice = 0,
                    };

                    BookingReservationService.SaveBookingReservation(booking);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                BookingDialogPopup.IsOpen = false;
                LoadBookingList();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            BookingDialogPopup.IsOpen = false;
            ResetInput();
            DgData.SelectedItem = null;
        }

        private void CreateDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetDetailInput();
            DgDataDetail.SelectedItem = null;
            BookingDetailPopup.IsOpen = true;
        }

        private void UpdateDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DgDataDetail.SelectedItem == null)
            {
                ViewDetailPopUp.IsOpen = false;
                MessageBox.Show("Please select a booking detail to update.");
            }
            else BookingDetailPopup.IsOpen = true;
        }

        private void DeleteDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgDataDetail.SelectedItem != null && DgDataDetail.SelectedItem is BookingDetail selectedBookingDetail)
                {
                    ViewDetailPopUp.IsOpen = false;
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this booking detail?",
                        "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        BookingDetailService.DeleteBookingDetail(selectedBookingDetail);
                        BookingReservation bookingReservation = BookingReservationService.GetBookingReservationById(bookingId);
                        bookingReservation.TotalPrice -= selectedBookingDetail.ActualPrice;
                        BookingReservationService.UpdateBookingReservation(bookingReservation);
                    }
                }
                else
                {
                    ViewDetailPopUp.IsOpen = false;
                    MessageBox.Show("Please select a booking detail to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DgDataDetail.SelectedItem = null;
                ResetDetailInput();
                LoadBookingList();
            }
        }

        private void SaveBookingDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!StartDate.SelectedDate.HasValue || !EndDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn ngày bắt đầu và ngày kết thúc.", "Lỗi");
                    return;
                }

                if (ComboRoom.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng.", "Lỗi");
                    return;
                }

                int numberOfDays = (EndDate.SelectedDate.Value - StartDate.SelectedDate.Value).Days;
                decimal pricePerDay = (decimal)RoomInformationService.GetRoomInformationById((int)ComboRoom.SelectedValue).RoomPricePerDay;

                BookingReservation bookingReservation = BookingReservationService.GetBookingReservationById(bookingId);

                if (DgDataDetail.SelectedItem != null && DgDataDetail.SelectedItem is BookingDetail selectedBookingDetail)
                {
                    selectedBookingDetail.StartDate = StartDate.SelectedDate.Value;
                    selectedBookingDetail.EndDate = EndDate.SelectedDate.Value;
                    selectedBookingDetail.RoomId = (int)ComboRoom.SelectedValue;
                    selectedBookingDetail.ActualPrice = pricePerDay * numberOfDays;

                    BookingDetailService.UpdateBookingDetail(selectedBookingDetail);
                }
                else
                {
                    BookingDetail bookingDetail = new BookingDetail
                    {
                        StartDate = StartDate.SelectedDate.Value,
                        EndDate = EndDate.SelectedDate.Value,
                        RoomId = (int)ComboRoom.SelectedValue,
                        BookingReservationId = bookingId,
                        ActualPrice = numberOfDays * pricePerDay,
                    };

                    BookingDetailService.SaveBookingDetail(bookingDetail);
                }

                BookingDetails = BookingDetailService.GetAllByBookingId(bookingId);
                DgDataDetail.ItemsSource = BookingDetails;

                decimal? totalPrice = 0;
                if (BookingDetails != null && BookingDetails.Any())
                {
                    totalPrice = BookingDetails.Sum(d => d.ActualPrice);
                }
                bookingReservation.TotalPrice = totalPrice;
                BookingReservationService.UpdateBookingReservation(bookingReservation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
            finally
            {
                BookingDetailPopup.IsOpen = false;
                LoadBookingList();
            }
        }


        private void CancelBookingDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            BookingDetailPopup.IsOpen = false;
            ResetDetailInput();
            DgDataDetail.SelectedItem = null;
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewDetailPopUp.IsOpen = false;
            BookingDetails = null!;
            DgData.SelectedItem = null;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilteredBookings = BookingReservations.Where(r =>
                (string.IsNullOrEmpty(TxtSearchCustomerName.Text) || r.Customer.CustomerFullName.ToUpper().Contains(TxtSearchCustomerName.Text.ToUpper())) &&
                (string.IsNullOrEmpty(TxtMinPrice.Text) || (r.TotalPrice != null && r.TotalPrice >= Convert.ToDecimal(TxtMinPrice.Text))) &&
                (string.IsNullOrEmpty(TxtMaxPrice.Text) || (r.TotalPrice != null && r.TotalPrice <= Convert.ToDecimal(TxtMaxPrice.Text)))
            ).ToList();

            DgData.ItemsSource = FilteredBookings;
        }
    }
}
