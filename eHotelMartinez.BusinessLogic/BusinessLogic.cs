using eHotelMartinez.BusinessLogic.Functions.Attraction;
using eHotelMartinez.BusinessLogic.Functions.Category;
using eHotelMartinez.BusinessLogic.Functions.Favorite;
using eHotelMartinez.BusinessLogic.Functions.Order;
using eHotelMartinez.BusinessLogic.Functions.Product;
using eHotelMartinez.BusinessLogic.Functions.Room;
using eHotelMartinez.BusinessLogic.Functions.Users;
using eHotelMartinez.BusinessLogic.Interfaces;

namespace eHotelMartinez.BusinessLogic
{
    public class BusinessLogic
    {
        public IProductActions GetProductActions()
        {
            return new ProductFlow();
        }

        public ICategoryActions GetCategoryActions()
        {
            return new CategoryFlow();
        }

        public IUserActions GetUserActions()
        {
            return new UserFlow();
        }
        public IOrderActions GetOrderActions()
        {
            return new OrderFlow();
        }

        public IAttractionActions GetAttractionActions()
        {
            return new AttractionFlow();
        }

        public IRoomActions GetRoomActions()
        {
            return new RoomFlow();
        }

        public IFavoriteActions GetFavoriteActions()
        {
            return new FavoriteFlow();
        }
    }
}
