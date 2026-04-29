using eHotelMartinez.BusinessLogic.Functions.Categories;
using eHotelMartinez.BusinessLogic.Functions.Products;
using eHotelMartinez.BusinessLogic.Functions.Session;
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

        public ISessionAction GetSessionActions()
        {
            return new SessionFlow();
        }

        public IUserActions GetUserActions()
        {
            return new UserFlow();
        }
    }
}
