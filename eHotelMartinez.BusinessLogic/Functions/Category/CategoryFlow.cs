using eHotelMartinez.BusinessLogic.Core.Category;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Category;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Entities.Category;

namespace eHotelMartinez.BusinessLogic.Functions.Category
{
    public class CategoryFlow : CategoryActions, ICategoryActions
    {
        public async Task<List<CategoryDTO>> GetAllCategoriesAction()
        {
            return await ExecuteGetAllCategoriesAction();
        }
        public async Task<CategoryDTO> GetCategoryByIdAction(int id)
        {
            return await ExecuteGetCategoryByIdAction(id);
        }
        public async Task<ResponseAction> ResponseCategoryCreateAction(CreateCategoryDTO category)
        {
            return await ExecuteCategoryCreateAction(category);
        }
        public async Task<ResponseMsg> ResponseCategoryUpdateAction(CategoryData category)
        {
            return await ExecuteCategoryUpdateAction(category);
        }
        public async Task<ResponseMsg> ResponseCategoryDeleteAction(int id)
        {
            return await ExecuteCategoryDeleteAction(id);
        }
    }
}