using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IBookingReservationService
    {
        List<BookingReservation> GetAll();
        BookingReservation GetBookingReservationById(int id);
        List<BookingReservation> GetBookingReservationsOfCustomer(int id);
        void SaveBookingReservation(BookingReservation bookingReservation);
        void UpdateBookingReservation(BookingReservation bookingReservation);
        void DeleteBookingReservation(BookingReservation bookingReservation);
    }
}
