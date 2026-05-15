using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Favorite;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IFavoriteActions
    {
        public Task<List<FavoriteDTO>> GetFavoritesByUserId(int userId);
        public Task<ResponseAction> AddFavorite(CreateFavoriteDTO favorite);
        public Task<ResponseMsg> RemoveFavorite(int favoriteId);
    }
}