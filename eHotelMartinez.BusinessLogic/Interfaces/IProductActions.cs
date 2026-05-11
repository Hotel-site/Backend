using eHotelMartinez.Domain.Models.Product;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IProductActions 
    {
        Task<List<ProductDTO>> GetAllProductsAction();
        Task<ProductDTO> GetProductByIdAction(int id);
        Task<ResponseAction> ResponseProductCreateAction(CreateProductDTO product);
        Task<ResponseMsg> ResponseProductUpdateAction(UpdateProductDTO product);
        Task<ResponseMsg> ResponseProductDeleteAction(int id);
    }
}
