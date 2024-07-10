using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository RoomTypeRepository;

        public RoomTypeService()
        {
            this.RoomTypeRepository = new RoomTypeRepository();
        }

        public List<RoomType> GetAll()
        {
            return this.RoomTypeRepository.GetAll();
        }

        public RoomType GetRoomTypeById(int id)
        {
            return this.RoomTypeRepository.GetRoomTypeById(id);
        }

        public void SaveRoomType(RoomType roomType)
        {
            this.RoomTypeRepository.SaveRoomType(roomType);
        }

        public void UpdateRoomType(RoomType roomType)
        {
            this.RoomTypeRepository.UpdateRoomType(roomType);
        }
        public void DeleteRoomType(RoomType roomType)
        {
            this.RoomTypeRepository.DeleteRoomType(roomType);
        }
    }
}