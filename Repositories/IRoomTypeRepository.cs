using BusinessObjects.Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IRoomTypeRepository
    {
        List<RoomType> GetAll();
        RoomType GetRoomTypeById(int id);
        void SaveRoomType(RoomType roomType);
        void UpdateRoomType(RoomType roomType);
        void DeleteRoomType(RoomType roomType);
    }
}
