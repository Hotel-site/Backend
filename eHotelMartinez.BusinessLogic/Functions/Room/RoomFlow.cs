using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.BusinessLogic.Core.Room;
using eHotelMartinez.Domain.Models.Room;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Functions.Room
{
    public class RoomFlow : RoomActions, IRoomActions
    {
        public List<RoomDTO> GetAllRoomsAction()
        {
            return ExecuteGetAllRooms();
        }

        public RoomDTO GetRoomByIdAction(int id)
        {
            return ExecuteGetRoomById(id);
        }

        public ResponseAction ResponseRoomCreateAction(CreateRoomDTO room)
        {
            return ExecuteCreateRoom(room);
        }

        public ResponseMsg ResponseRoomUpdateAction(RoomDTO room)
        {
            return ExecuteUpdateRoom(room);
        }

        public ResponseMsg ResponseRoomDeleteAction(int id)
        {
            return ExecuteDeleteRoom(id);
        }
    }
}
