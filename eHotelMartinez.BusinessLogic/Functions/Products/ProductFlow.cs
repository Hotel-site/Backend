using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eHotelMartinez.BusinessLogic.Core.Products;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Product;


namespace eHotelMartinez.BusinessLogic.Functions.Products
{
    public class ProductFlow : ProductActions, IProductActions
    {
        List<ProductDTO> IProductActions.ExecuteGetAllProducts()
        {
            return ExecuteGetAllProducts();
        }

        ProductDTO IProductActions.ExecuteGetProductById(int id)
        {
            return ExecuteGetProductById(id);
        }

        ResponseMsg IProductActions.ExecuteCreateProductAction(ProductDTO product)
        {
            return ExecuteCreateProductAction(product);
        }

        ResponseMsg IProductActions.ExecuteUpdateProductAction(UpdateProductDTO product)
        {
            return ExecuteUpdateProductAction(product);
        }

        ResponseMsg IProductActions.ExecuteDeleteProductAction(int id)
        {
            return ExecuteDeleteProductAction(id);
        }
    }
}
