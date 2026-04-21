using eHotelMartinez.BusinessLogic.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eHotelMartinez.Domain.Models.Product;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Interfacies
{
    public interface IProductActions 
    {
        List<ProductDTO> ExecuteGetAllProducts();
        ProductDTO ExecuteGetProductById(int id);
        ResponseMsg ExecuteCreateProductAction(ProductDTO product);
        ResponseMsg ExecuteUpdateProductAction(UpdateProductDTO product);
        ResponseMsg ExecuteDeleteProductAction(int id);
    }
}
