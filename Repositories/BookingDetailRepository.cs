using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public List<BookingDetail> GetAll()
            => BookingDetailDAO.Instance.GetAll();

        public void SaveBookingDetail(BookingDetail bookingDetail)
            => BookingDetailDAO.Instance.SaveBookingDetail(bookingDetail);

        public void UpdateBookingDetail(BookingDetail bookingDetail)
            => BookingDetailDAO.Instance.UpdateBookingDetail(bookingDetail);

        public void DeleteBookingDetail(BookingDetail bookingDetail)
            => BookingDetailDAO.Instance.DeleteBookingDetail(bookingDetail);
    }
}
