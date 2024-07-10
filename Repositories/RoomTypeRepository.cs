using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public List<RoomType> GetAll()
            => RoomTypeDAO.Instance.GetAll();

        public RoomType GetRoomTypeById(int id)
            => RoomTypeDAO.Instance.GetRoomTypeById(id);

        public void SaveRoomType(RoomType roomType)
            => RoomTypeDAO.Instance.SaveRoomType(roomType);

        public void UpdateRoomType(RoomType roomType)
            => RoomTypeDAO.Instance.UpdateRoomType(roomType);

        public void DeleteRoomType(RoomType roomType)
            => RoomTypeDAO.Instance.DeleteRoomType(roomType);
    }
}
