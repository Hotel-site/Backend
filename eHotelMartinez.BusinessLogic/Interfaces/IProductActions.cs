using eHotelMartinez.Domain.Models.Product;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Entities.Product;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IProductActions 
    {
        List<ProductDTO> GetAllProductsAction();
        ProductDTO GetProductByIdAction(int id);
        ResponseMsg ResponseProductCreateAction(ProductData product);
        ResponseMsg ResponseProductUpdateAction(UpdateProductDTO product);
        ResponseMsg ResponseProductDeleteAction(int id);
    }
}
