﻿using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class BookingReservationDAO
    {
        private static BookingReservationDAO instance = null!;
        private static readonly object lockObject = new object();

        private BookingReservationDAO() { }

        public static BookingReservationDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new BookingReservationDAO();
                    }
                    return instance;
                }
            }
        }

        public List<BookingReservation> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations.ToList();
        }

        public BookingReservation GetBookingReservationById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations.FirstOrDefault(b => b.BookingReservationId == id);
        }

        public List<BookingReservation> GetBookingReservationsOfCustomer(int customerId)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations.Where(b => b.CustomerId == customerId).ToList();
        }

        public void SaveBookingReservation(BookingReservation bookingReservation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.BookingReservations.Add(bookingReservation);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBookingReservation(BookingReservation bookingReservation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Entry<BookingReservation>(bookingReservation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteBookingReservation(BookingReservation bookingReservation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var reservation = db.BookingReservations.SingleOrDefault(b => b.BookingReservationId == bookingReservation.BookingReservationId);
                if (reservation != null)
                {
                    db.BookingReservations.Remove(reservation);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookingReservation> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            using var db = new FuminiHotelManagementContext();
            return db.BookingReservations
                .Include(br => br.Customer)
                .Where(br => br.BookingDate >= startDate && br.BookingDate <= endDate)
                .OrderByDescending(br => br.BookingDate)
                .ToList();
        }
    }
}
