using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IBookingDetailService
    {
        List<BookingDetail> GetAll();
        List<BookingDetail> GetAllByRoomId(int roomId);
        List<BookingDetail> GetAllByBookingId(int bookingId);
        void SaveBookingDetail(BookingDetail bookingDetail);
        void UpdateBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(BookingDetail bookingDetail);
    }
}
