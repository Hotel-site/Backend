using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.BusinessLogic.Core.Room;
using eHotelMartinez.Domain.Models.Room;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Functions.Room
{
    public class RoomFlow : RoomActions, IRoomActions
    {
        public async Task<List<RoomDTO>> GetAllRoomsAction()
        {
            return await ExecuteGetAllRooms();
        }

        public async Task<RoomDTO> GetRoomByIdAction(int id)
        {
            return await ExecuteGetRoomById(id);
        }

        public async Task<ResponseAction> ResponseRoomCreateAction(CreateRoomDTO room)
        {
            return await ExecuteCreateRoom(room);
        }

        public async Task<ResponseMsg> ResponseRoomUpdateAction(RoomDTO room)
        {
            return await ExecuteUpdateRoom(room);
        }

        public async Task<ResponseMsg> ResponseRoomDeleteAction(int id)
        {
            return await ExecuteDeleteRoom(id);
        }
    }
}
