using BusinessObjects.Models;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class RoomInformationRepository : IRoomInformationRepository
    {
        public List<RoomInformation> GetAll()
            => RoomInformationDAO.Instance.GetAll();

        public RoomInformation GetRoomInformationById(int id)
            => RoomInformationDAO.Instance.GetRoomInformationById(id);

        public void SaveRoomInformation(RoomInformation roomInformation)
            => RoomInformationDAO.Instance.SaveRoomInformation(roomInformation);

        public void UpdateRoomInformation(RoomInformation roomInformation)
            => RoomInformationDAO.Instance.UpdateRoomInformation(roomInformation);

        public void DeleteRoomInformation(RoomInformation roomInformation)
            => RoomInformationDAO.Instance.DeleteRoomInformation(roomInformation);
    }
}
