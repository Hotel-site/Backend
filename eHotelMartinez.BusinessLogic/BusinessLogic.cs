using eHotelMartinez.BusinessLogic.Functions.Products;
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
        public IUserActions GetUserActions()
        {
            return new UserFlow();
        }
    }
}
