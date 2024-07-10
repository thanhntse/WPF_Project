using BusinessObjects.Models;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class RoomInformationService : IRoomInformationService
    {
        private readonly IRoomInformationRepository RoomInformationRepository;

        public RoomInformationService()
        {
            this.RoomInformationRepository = new RoomInformationRepository();
        }

        public List<RoomInformation> GetAll()
        {
            return this.RoomInformationRepository.GetAll();
        }

        public RoomInformation GetRoomInformationById(int id)
        {
            return this.RoomInformationRepository.GetRoomInformationById(id);
        }

        public void SaveRoomInformation(RoomInformation roomInformation)
        {
            this.RoomInformationRepository.SaveRoomInformation(roomInformation);
        }

        public void UpdateRoomInformation(RoomInformation roomInformation)
        {
            this.RoomInformationRepository.UpdateRoomInformation(roomInformation);
        }

        public void DeleteRoomInformation(RoomInformation roomInformation)
        {
            this.RoomInformationRepository.DeleteRoomInformation(roomInformation);
        }
    }
}
