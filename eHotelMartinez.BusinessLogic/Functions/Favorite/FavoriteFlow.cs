using eHotelMartinez.BusinessLogic.Core.Favorite;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Favorite;

namespace eHotelMartinez.BusinessLogic.Functions.Favorite
{
    public class FavoriteFlow : FavoriteActions, IFavoriteAction
    {
        public async Task<List<FavoriteDTO>> GetFavoritesByUserId(int userId)
        {
            return await ExecuteGetFavoritesByUserId(userId);
        }
        public async Task<ResponseAction> AddFavorite(CreateFavoriteDTO favorite)
        {
            return await ExecuteAddFavorite(favorite);
        }
        public async Task<ResponseMsg> RemoveFavorite(int favoriteId)
        {
            return await ExecuteRemoveFavorite(favoriteId);
        }
    }
}
