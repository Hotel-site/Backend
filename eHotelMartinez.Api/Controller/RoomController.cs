using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Room;
using eHotelMartinez.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomActions _roomActions;
        public RoomController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _roomActions = bl.GetRoomActions();
        }

        [HttpGet("all")]
        public IActionResult GetAllRooms()
        {
            var rooms = _roomActions.GetAllRoomsAction();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetRooms(int id)
        {
            var room = _roomActions.GetRoomByIdAction(id);

            if (room == null)
            {
                return NotFound(new
                {
                    Message = $"Room with ID {id} Not Found!"
                });
            }
            return Ok(room);
        }

        [AdminOnly]
        [HttpPost]
        public IActionResult CreateRoom([FromBody] CreateRoomDTO room)
        {
            var response = _roomActions.ResponseRoomCreateAction(room);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AdminOnly]
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] RoomDTO room)
        {
            room.Id = id;
            var response = _roomActions.ResponseRoomUpdateAction(room);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AdminOnly]
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var response = _roomActions.ResponseRoomDeleteAction(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
