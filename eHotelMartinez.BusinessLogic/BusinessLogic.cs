using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.BusinessLogic.Functions.Products;

namespace eHotelMartinez.BusinessLogic
{
    public class BusinessLogic
    {

        public IProductActions GetProductActions()
        {
            return new ProductFlow();
        }


    }
}
