using eHotelMartinez.Domain.Entities.Room;
using eHotelMartinez.Domain.Models.Room;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using eHotelMartinez.Domain.Enums;


namespace eHotelMartinez.BusinessLogic.Core.Room
{
    public class RoomActions
    {
        protected async Task<List<RoomDTO>> ExecuteGetAllRooms()
        {
            await using var db = new RoomContext();

            var rooms = await db.Rooms
                .AsNoTracking()
                .Include(r => r.Images)
                .Where(r => r.Status != RoomStatus.Inactive)
                .ToListAsync();
            return rooms.Select(r => new RoomDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Amenities = r.Amenities?.ToList() ?? new List<string>(),
                Status = r.Status,
                Images = r.Images.Select(i => new RoomImageDTO
                {
                    Url = i.Url
                }).ToList(),
                Price = r.Price
            }).ToList();
        }

        protected async Task<RoomDTO> ExecuteGetRoomById(int id)
        {
            await using var db = new RoomContext();
            var r = await db.Rooms
                .AsNoTracking()
                .Include(r => r.Images)
                .FirstOrDefaultAsync(r => r.Id == id && r.Status != RoomStatus.Inactive);

            if (r == null)
                return null;

            return new RoomDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Amenities = r.Amenities?.ToList() ?? new List<string>(),
                Status = r.Status,
                Images = r.Images.Select(i => new RoomImageDTO
                {
                    Url = i.Url
                }).ToList(),
                Price = r.Price
            };
        }

        protected async Task<ResponseAction> ExecuteCreateRoom(CreateRoomDTO newRoom)
        {
            if (string.IsNullOrWhiteSpace(newRoom.Name))
                return new ResponseAction { IsSuccess = false, Message = "Room name is required." };

            if (newRoom.Price < 0)
                return new ResponseAction { IsSuccess = false, Message = "Price must be greater than zero." };

            var roomData = new RoomData
            {
                Name = newRoom.Name,
                Description = newRoom.Description,

                Amenities = (newRoom.Amenities ?? new List<string>())
                    .Select(a => a?.Trim())
                    .Where(a => a is { Length: > 0 })
                    .Select(a => a!)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList(),

                Images = newRoom.Images?.Select(i => new RoomImageData
                {
                    Url = i.Url
                }).ToList() ?? new List<RoomImageData>(),
                Price = newRoom.Price,
                Status = RoomStatus.Available
            };

            await using var db = new RoomContext();
            db.Rooms.Add(roomData);
            await db.SaveChangesAsync();

            return new ResponseAction
            {
                IsSuccess = true,
                Message = $"Room created successfully.",
                Id = roomData.Id
            };
        }

        protected async Task<ResponseMsg> ExecuteUpdateRoom(RoomDTO updatedRoom)
        {
            await using var db = new RoomContext();
            var room = await db.Rooms
                .Include(r => r.Images)
                .FirstOrDefaultAsync(r => r.Id == updatedRoom.Id);

            if (room == null)
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Room not found."
                };

            if (string.IsNullOrWhiteSpace(updatedRoom.Name))
                return new ResponseMsg { IsSuccess = false, Message = "Room name is required." };

            if (updatedRoom.Price < 0)
                return new ResponseMsg { IsSuccess = false, Message = "Price must be greater than zero." };

            if (!string.IsNullOrWhiteSpace(updatedRoom.Name))
                room.Name = updatedRoom.Name;

            room.Description = updatedRoom.Description;

            if (updatedRoom.Amenities != null)
            {
                var amenities = updatedRoom.Amenities
                    .Select(a => a?.Trim())
                    .Where(a => !string.IsNullOrWhiteSpace(a))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();
                room.Amenities = amenities;
            }

            var existingUrls = room.Images
                .Where(i => i.IsActive)
                .Select(i => i.Url)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var dtoImage in updatedRoom.Images ?? new())
            {
                var url = dtoImage.Url?.Trim();
                if (string.IsNullOrWhiteSpace(url))
                    continue;

                if (existingUrls.Add(url))
                    room.Images.Add(new RoomImageData { Url = url, IsActive = true });
            }

            if (updatedRoom.Price > 0)
                room.Price = updatedRoom.Price;

            if (updatedRoom.Status != null)
                room.Status = updatedRoom.Status;

            await db.SaveChangesAsync();
            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Room updated successfully."
            };
        }

        protected async Task<ResponseMsg> ExecuteDeleteRoom(int id)
        {
            await using var db = new RoomContext();
            var room = await db.Rooms
                .Include(r => r.Images)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Room not found."
                };

            room.Status = RoomStatus.Inactive;

            foreach (var image in room.Images)
            {
                image.IsActive = false;
            }

            await db.SaveChangesAsync();

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Room deleted successfully."
            };
        }
    }
}
