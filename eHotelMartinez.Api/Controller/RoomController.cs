using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Room;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/room")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private IRoomActions _roomActions;
        public RoomController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _roomActions = bl.GetRoomActions();
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public IActionResult GetAllRooms()
        {
            var rooms = _roomActions.GetAllRoomsAction();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRoom([FromBody] CreateRoomDTO room)
        {
            var response = _roomActions.ResponseRoomCreateAction(room);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
