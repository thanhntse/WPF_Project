using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class RoomInformationDAO
    {
        private static RoomInformationDAO instance = null!;
        private static readonly object lockObject = new object();

        private RoomInformationDAO() { }

        public static RoomInformationDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new RoomInformationDAO();
                    }
                    return instance;
                }
            }
        }

        public List<RoomInformation> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return db.RoomInformations.ToList();
        }

        public RoomInformation GetRoomInformationById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.RoomInformations.FirstOrDefault(r => r.RoomId == id);
        }

        public void SaveRoomInformation(RoomInformation roomInformation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.RoomInformations.Add(roomInformation);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRoomInformation(RoomInformation roomInformation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Entry<RoomInformation>(roomInformation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRoomInformation(RoomInformation roomInformation)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var room = db.RoomInformations.SingleOrDefault(r => r.RoomId == roomInformation.RoomId);
                if (room != null)
                {
                    db.RoomInformations.Remove(room);
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
