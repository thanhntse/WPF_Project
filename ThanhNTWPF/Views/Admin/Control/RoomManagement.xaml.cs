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
        public List<RoomInformation> RoomInformations { get; set; }
        public RoomManagement()
        {
            InitializeComponent();
            RoomInformationService = new RoomInformationService();
            RoomInformations = RoomInformationService.GetAll();
            this.DataContext = this;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
