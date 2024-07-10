using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IRoomInformationService
    {
        List<RoomInformation> GetAll();
        RoomInformation GetRoomInformationById(int id);
        void SaveRoomInformation(RoomInformation roomInformation);
        void UpdateRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformation(RoomInformation roomInformation);
    }
}
