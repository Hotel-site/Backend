using eHotelMartinez.BusinessLogic.Core.Products;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Product;


namespace eHotelMartinez.BusinessLogic.Functions.Products
{
    public class ProductFlow : ProductActions, IProductActions
    {
        public List<ProductDTO> GetAllProductsAction()
        {
            return ExecuteGetAllProducts();
        }
        public ProductDTO GetProductByIdAction(int id)
        {
            return ExecuteGetProductById(id);
        }
        public ResponseMsg ResponseProductCreateAction(CreateProductDTO product)
        {
            return ExecuteCreateProductAction(product);
        }
        public ResponseMsg ResponseProductUpdateAction(UpdateProductDTO product)
        {
            return ExecuteUpdateProductAction(product);
        }
        public ResponseMsg ResponseProductDeleteAction(int id)
        {
            return ExecuteDeleteProductAction(id);
        }

    }
}
