using eHotelMartinez.BusinessLogic.Core.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
