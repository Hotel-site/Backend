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
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomActions.GetAllRoomsAction();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRooms(int id)
        {
            var room = await _roomActions.GetRoomByIdAction(id);

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
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDTO room)
        {
            var response = await _roomActions.ResponseRoomCreateAction(room);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomDTO room)
        {
            room.Id = id;
            var response = await _roomActions.ResponseRoomUpdateAction(room);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var response = await _roomActions.ResponseRoomDeleteAction(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
