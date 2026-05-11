using eHotelMartinez.BusinessLogic.Core.Products;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Product;


namespace eHotelMartinez.BusinessLogic.Functions.Product
{
    public class ProductFlow : ProductActions, IProductActions
    {
        public async Task<List<ProductDTO>> GetAllProductsAction()
        {
            return await ExecuteGetAllProducts();
        }
        public async Task<ProductDTO> GetProductByIdAction(int id)
        {
            return await ExecuteGetProductById(id);
        }
        public async Task<ResponseAction> ResponseProductCreateAction(CreateProductDTO product)
        {
            return await ExecuteCreateProductAction(product);
        }
        public async Task<ResponseMsg> ResponseProductUpdateAction(UpdateProductDTO product)
        {
            return await ExecuteUpdateProductAction(product);
        }
        public async Task<ResponseMsg> ResponseProductDeleteAction(int id)
        {
            return await ExecuteDeleteProductAction(id);
        }

    }
}
