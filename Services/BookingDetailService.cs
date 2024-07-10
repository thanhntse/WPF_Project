using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IBookingDetailRepository BookingDetailRepository;

        public BookingDetailService()
        {
            this.BookingDetailRepository = new BookingDetailRepository();
        }

        public List<BookingDetail> GetAll()
        {
            return this.BookingDetailRepository.GetAll();
        }

        public void SaveBookingDetail(BookingDetail bookingDetail)
        {
            this.BookingDetailRepository.SaveBookingDetail(bookingDetail);
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            this.BookingDetailRepository.UpdateBookingDetail(bookingDetail);
        }

        public void DeleteBookingDetail(BookingDetail bookingDetail)
        {
            this.BookingDetailRepository.DeleteBookingDetail(bookingDetail);
        }

        public List<BookingDetail> GetAllByRoomId(int roomId)
        {
            return this.BookingDetailRepository.GetAllByRoomId(roomId);
        }
    }
}
