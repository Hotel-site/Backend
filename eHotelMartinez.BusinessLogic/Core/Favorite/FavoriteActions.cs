using eHotelMartinez.Domain.Models.Favorite;
using eHotelMartinez.Domain.Entities.Favorite;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using eHotelMartinez.Domain.Enums;
using eHotelMartinez.Domain.Entities.Product;
using eHotelMartinez.Domain.Entities.Attraction;

namespace eHotelMartinez.BusinessLogic.Core.Favorite
{
    public class FavoriteActions
    {
            protected async Task<List<FavoriteDTO>> ExecuteGetFavoritesByUserId(int userId)
            {
                await using var userDb = new UserContext();

            var favorites = await userDb.Favorites
                .Where(f => f.UserId == userId && f.IsActive)
                .ToListAsync();

            string userName;
            
                userName = await userDb.Users
                    .Where(u => u.Id == userId)
                    .Select(u => u.Username)
                    .FirstOrDefaultAsync() ?? "Unknown User";

            return favorites.Select(f => new FavoriteDTO
            {
                Id = f.Id,
                UserName = userName,
                EntityType = f.EntityType,
                EntityId = f.EntityId
            }).ToList();
        }

        protected async Task<ResponseAction> ExecuteAddFavorite(CreateFavoriteDTO favorite)
        {
            await using var userDb = new UserContext();
            await using var categoryDb = new CategoryContext();

            if (!await userDb.Users.AnyAsync(u => u.Id == favorite.UserId))
            {
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            bool entityExists = false;
            switch (favorite.EntityType)
            {
                case EntityType.Product:
                    entityExists = await categoryDb.Set<ProductData>().AnyAsync(p => p.Id == favorite.EntityId);
                    break;
                case EntityType.Attraction:
                    entityExists = await categoryDb.Set<AttractionData>().AnyAsync(a => a.Id == favorite.EntityId);
                    break;
            }

            if (!entityExists)
            {
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Entity not found"
                };
            }

            var existsFavorite = await userDb.Favorites.FirstOrDefaultAsync(f =>
                f.UserId == favorite.UserId &&
                f.EntityType == favorite.EntityType &&
                f.EntityId == favorite.EntityId);

            if (existsFavorite != null)
            {
                if (existsFavorite.IsActive)
                {
                    return new ResponseAction
                    {
                        IsSuccess = false,
                        Message = "Favorite already exists"
                    };
                }
                else
                {
                    existsFavorite.IsActive = true;
                    await userDb.SaveChangesAsync();

                    return new ResponseAction
                    {
                        IsSuccess = true,
                        Message = "Favorite activated succesfully",
                        Id = existsFavorite.Id
                    };
                }
            }

            var newFavorite = new FavoriteData
            {
                UserId = favorite.UserId,
                EntityType = favorite.EntityType,
                EntityId = favorite.EntityId
            };

            userDb.Favorites.Add(newFavorite);
            await  userDb.SaveChangesAsync();

            return new ResponseAction
            {
                IsSuccess = true,
                Message = "Favorite added successfully",
                Id = newFavorite.Id
            };
        }

        protected async Task<ResponseMsg> ExecuteRemoveFavorite(int favoriteId)
        {
            await using var userDb = new UserContext();

            var favorite = await userDb.Favorites.FindAsync(favoriteId);

            if (favorite == null)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Favorite not found"
                };
            }

            favorite.IsActive = false;

            await userDb.SaveChangesAsync();

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Favorite removed successfully"
            };
        }
    }
}
