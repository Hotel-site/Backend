using eHotelMartinez.Domain.Models.Room;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IRoomActions
    {
        Task<List<RoomDTO>> GetAllRoomsAction();
        Task<RoomDTO> GetRoomByIdAction(int id);
        Task<ResponseAction> ResponseRoomCreateAction(CreateRoomDTO room);
        Task<ResponseMsg> ResponseRoomUpdateAction(RoomDTO room);
        Task<ResponseMsg> ResponseRoomDeleteAction(int id);
    }
}
