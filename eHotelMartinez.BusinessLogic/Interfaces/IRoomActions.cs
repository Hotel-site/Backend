using eHotelMartinez.Domain.Models.Room;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IRoomActions
    {
        List<RoomDTO> GetAllRoomsAction();
        RoomDTO GetRoomByIdAction(int id);
        ResponseAction ResponseRoomCreateAction(CreateRoomDTO room);
        ResponseMsg ResponseRoomUpdateAction(RoomDTO room);
        ResponseMsg ResponseRoomDeleteAction(int id);

    }
}
