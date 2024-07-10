using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IBookingDetailService
    {
        List<BookingDetail> GetAll();
        void SaveBookingDetail(BookingDetail bookingDetail);
        void UpdateBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(BookingDetail bookingDetail);
    }
}
