using eHotelMartinez.Domain.Models.Product;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IProductActions 
    {
        List<ProductDTO> GetAllProductsAction();
        ProductDTO GetProductByIdAction(int id);
        ResponseMsg ResponseProductCreateAction(ProductDTO product);
        ResponseMsg ResponseProductUpdateAction(UpdateProductDTO product);
        ResponseMsg ResponseProductDeleteAction(int id);
    }
}
