using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.BusinessLogic.Functions.Products;
using eHotelMartinez.BusinessLogic.Functions.Session;

namespace eHotelMartinez.BusinessLogic
{
    public class BusinessLogic
    {
        public IProductActions GetProductActions()
        {
            return new ProductFlow();
        }
        
        public ISessionAction GetSessionActions()
        {
            return new SessionFlow();
        }


    }
}
