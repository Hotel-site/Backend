using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Category;
using eHotelMartinez.Domain.Entities.Category;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface ICategoryActions
    {
        Task<List<CategoryDTO>> GetAllCategoriesAction();
        Task<CategoryDTO> GetCategoryByIdAction(int id);
        Task<ResponseAction> ResponseCategoryCreateAction(CreateCategoryDTO category);
        Task<ResponseMsg> ResponseCategoryUpdateAction(CategoryData category);
        Task<ResponseMsg> ResponseCategoryDeleteAction(int id);
    }
}
