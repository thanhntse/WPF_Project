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
    /// Interaction logic for RoomManagement.xaml
    /// </summary>
    public partial class RoomManagement : UserControl
    {
        private readonly IRoomInformationService RoomInformationService;
        private readonly IRoomTypeService RoomTypeService;
        private readonly IBookingDetailService BookingDetailService;
        public List<RoomInformation> RoomInformations { get; set; } = null!;
        public List<RoomInformation> FilterRooms { get; set; } = null!;
        public List<RoomType> RoomTypes { get; set; } = null!;

        public RoomManagement()
        {
            InitializeComponent();
            RoomInformationService = new RoomInformationService();
            RoomTypeService = new RoomTypeService();
            BookingDetailService = new BookingDetailService();
            LoadRoomList();
            RoomTypes = RoomTypeService.GetAll();
            this.DataContext = this;
        }

        private void ResetInput()
        {
            TxtRoomNumber.Text = "";
            TxtRoomDetail.Text = "";
            TxtMaxCapacity.Text = "";
            TxtPricePerDay.Text = "";
            ComboRoomType.SelectedIndex = -1;
            RbActive.IsChecked = false;
            RbInactive.IsChecked = false;
        }

        public void LoadRoomList()
        {
            try
            {
                RoomInformations = RoomInformationService.GetAll();
                RoomInformations.ForEach(
                    r =>
                    {
                        r.RoomType = RoomTypeService.GetRoomTypeById(r.RoomTypeId);
                    }
                );
                FilterRooms = RoomInformations;
                DgData.ItemsSource = FilterRooms;
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
            if (DgData.SelectedItem != null && DgData.SelectedItem is RoomInformation selectedRoom)
            {
                TxtRoomNumber.Text = selectedRoom.RoomNumber;
                TxtRoomDetail.Text = selectedRoom.RoomDetailDescription;
                TxtMaxCapacity.Text = selectedRoom.RoomMaxCapacity?.ToString();
                TxtPricePerDay.Text = selectedRoom.RoomPricePerDay?.ToString();

                // Set ComboBox for Room Type
                ComboRoomType.SelectedValue = selectedRoom.RoomTypeId;

                // Set RadioButton for Room Status
                if (selectedRoom.RoomStatus == 1)
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

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            ResetInput();
            DgData.SelectedItem = null;
            RoomDialogPopup.IsOpen = true;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DgData.SelectedItem == null)
            {
                MessageBox.Show("Please select a room to update.");
            }
            else RoomDialogPopup.IsOpen = true;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgData.SelectedItem != null && DgData.SelectedItem is RoomInformation selectedRoom)
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete room with ID {selectedRoom.RoomId}?",
                        "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        List<BookingDetail> bookingDetails = BookingDetailService.GetAllByRoomId(selectedRoom.RoomId);
                        if (bookingDetails.Count != 0)
                        {
                            selectedRoom.RoomStatus = 0;
                            RoomInformationService.UpdateRoomInformation(selectedRoom);
                        }
                        else
                        {
                            RoomInformationService.DeleteRoomInformation(selectedRoom);
                        }
                        LoadRoomList();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a room to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgData.SelectedItem != null && DgData.SelectedItem is RoomInformation selectedRoom)
                {
                    selectedRoom.RoomNumber = TxtRoomNumber.Text;
                    selectedRoom.RoomDetailDescription = TxtRoomDetail.Text;
                    selectedRoom.RoomMaxCapacity = string.IsNullOrEmpty(TxtMaxCapacity.Text) ? null : (int?)Convert.ToInt32(TxtMaxCapacity.Text);
                    selectedRoom.RoomPricePerDay = string.IsNullOrEmpty(TxtPricePerDay.Text) ? null : (decimal?)Convert.ToDecimal(TxtPricePerDay.Text);
                    selectedRoom.RoomTypeId = (int)ComboRoomType.SelectedValue;
                    selectedRoom.RoomStatus = RbActive.IsChecked == true ? (byte)1 : (byte)0;

                    RoomInformationService.UpdateRoomInformation(selectedRoom);
                }
                else
                {
                    RoomInformation room = new RoomInformation
                    {
                        RoomNumber = TxtRoomNumber.Text,
                        RoomDetailDescription = TxtRoomDetail.Text,
                        RoomMaxCapacity = string.IsNullOrEmpty(TxtMaxCapacity.Text) ? null : (int?)Convert.ToInt32(TxtMaxCapacity.Text),
                        RoomPricePerDay = string.IsNullOrEmpty(TxtPricePerDay.Text) ? null : (decimal?)Convert.ToDecimal(TxtPricePerDay.Text),
                        RoomTypeId = (int)ComboRoomType.SelectedValue,
                        RoomStatus = RbActive.IsChecked == true ? (byte)1 : (byte)0
                    };

                    RoomInformationService.SaveRoomInformation(room);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                RoomDialogPopup.IsOpen = false;
                LoadRoomList();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            RoomDialogPopup.IsOpen = false;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterRooms = RoomInformations.Where(r =>
                (string.IsNullOrEmpty(TxtSearchRoomNumber.Text) || r.RoomNumber.ToUpper().Contains(TxtSearchRoomNumber.Text.ToUpper())) &&
                (string.IsNullOrEmpty(TxtSearchMaxCapacity.Text) || (r.RoomMaxCapacity != null && r.RoomMaxCapacity.ToString().Contains(TxtSearchMaxCapacity.Text))) &&
                (string.IsNullOrEmpty(TxtSearchRoomType.Text) || r.RoomType.RoomTypeName.ToUpper().Contains(TxtSearchRoomType.Text.ToUpper())) &&
                (string.IsNullOrEmpty(TxtMinPrice.Text) || (r.RoomPricePerDay != null && r.RoomPricePerDay >= Convert.ToDecimal(TxtMinPrice.Text))) &&
                (string.IsNullOrEmpty(TxtMaxPrice.Text) || (r.RoomPricePerDay != null && r.RoomPricePerDay <= Convert.ToDecimal(TxtMaxPrice.Text)))
            ).ToList();

            DgData.ItemsSource = FilterRooms;
        }


    }
}
