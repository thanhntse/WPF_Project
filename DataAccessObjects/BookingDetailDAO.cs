using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class BookingDetailDAO
    {
        private static BookingDetailDAO instance = null!;
        private static readonly object lockObject = new object();

        private BookingDetailDAO() { }

        public static BookingDetailDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new BookingDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public List<BookingDetail> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingDetails.ToList();
        }

        public List<BookingDetail> GetAllByRoomId(int roomId)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingDetails.Where(bd => bd.RoomId == roomId).ToList();
        }

        public void SaveBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.BookingDetails.Add(bookingDetail);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Entry<BookingDetail>(bookingDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var detail = db.BookingDetails.SingleOrDefault(
                        b => b.BookingReservationId == bookingDetail.BookingReservationId
                            && b.RoomId == bookingDetail.RoomId
                    );
                if (detail != null)
                {
                    db.BookingDetails.Remove(detail);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
