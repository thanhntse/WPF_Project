using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class RoomTypeDAO
    {
        private static RoomTypeDAO instance = null!;
        private static readonly object lockObject = new object();

        private RoomTypeDAO() { }

        public static RoomTypeDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new RoomTypeDAO();
                    }
                    return instance;
                }
            }
        }

        public List<RoomType> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return db.RoomTypes.ToList();
        }

        public RoomType GetRoomTypeById(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.RoomTypes.FirstOrDefault(r => r.RoomTypeId == id);
        }

        public void SaveRoomType(RoomType roomType)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.RoomTypes.Add(roomType);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRoomType(RoomType roomType)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                db.Entry<RoomType>(roomType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRoomType(RoomType roomType)
        {
            try
            {
                using var db = new FuminiHotelManagementContext();
                var room = db.RoomTypes.SingleOrDefault(r => r.RoomTypeId == roomType.RoomTypeId);
                if (room != null)
                {
                    db.RoomTypes.Remove(room);
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
