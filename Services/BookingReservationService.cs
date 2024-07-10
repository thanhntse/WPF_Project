using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class BookingReservationService : IBookingReservationService
    {
        private readonly IBookingReservationRepository BookingReservationRepository;

        public BookingReservationService()
        {
            this.BookingReservationRepository = new BookingReservationRepository();
        }

        public List<BookingReservation> GetAll()
        {
            return this.BookingReservationRepository.GetAll();
        }

        public BookingReservation GetBookingReservationById(int id)
        {
            return this.BookingReservationRepository.GetBookingReservationById(id);
        }

        public void SaveBookingReservation(BookingReservation bookingReservation)
        {
            this.BookingReservationRepository.SaveBookingReservation(bookingReservation);
        }

        public void UpdateBookingReservation(BookingReservation bookingReservation)
        {
            this.BookingReservationRepository.UpdateBookingReservation(bookingReservation);
        }

        public void DeleteBookingReservation(BookingReservation bookingReservation)
        {
            this.BookingReservationRepository.DeleteBookingReservation(bookingReservation);
        }
    }
}
