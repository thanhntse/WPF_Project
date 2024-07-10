using BusinessObjects.Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IBookingDetailRepository
    {
        List<BookingDetail> GetAll();
        void SaveBookingDetail(BookingDetail bookingDetail);
        void UpdateBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(BookingDetail bookingDetail);
    }
}
