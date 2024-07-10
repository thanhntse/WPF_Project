using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public List<BookingReservation> GetAll()
            => BookingReservationDAO.Instance.GetAll();

        public BookingReservation GetBookingReservationById(int id)
            => BookingReservationDAO.Instance.GetBookingReservationById(id);

        public void SaveBookingReservation(BookingReservation bookingReservation)
            => BookingReservationDAO.Instance.SaveBookingReservation(bookingReservation);

        public void UpdateBookingReservation(BookingReservation bookingReservation)
            => BookingReservationDAO.Instance.UpdateBookingReservation(bookingReservation);

        public void DeleteBookingReservation(BookingReservation bookingReservation)
            => BookingReservationDAO.Instance.DeleteBookingReservation(bookingReservation);

        public List<BookingReservation> GetBookingReservationsOfCustomer(int id)
            => BookingReservationDAO.Instance.GetBookingReservationsOfCustomer(id);
    }
}
