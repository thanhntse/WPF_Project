using BusinessObjects.Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IRoomInformationRepository
    {
        List<RoomInformation> GetAll();
        RoomInformation GetRoomInformationById(int id);
        void SaveRoomInformation(RoomInformation roomInformation);
        void UpdateRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformation(RoomInformation roomInformation);
    }
}
