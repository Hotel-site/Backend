using eHotelMartinez.BusinessLogic.Core.Category;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Category;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Entities.Category;

namespace eHotelMartinez.BusinessLogic.Functions.Categories
{
    public class CategoryFlow : CategoryActions, ICategoryActions
    {
        public List<CategoryDTO> GetAllCategoriesAction()
        {
            return ExecuteGetAllCategoriesAction();
        }
        public CategoryDTO GetCategoryByIdAction(int id)
        {
            return ExecuteGetCategoryByIdAction(id);
        }
        public ResponseMsg ResponseCategoryCreateAction(CreateCategoryDTO category)
        {
            return ExecuteCategoryCreateAction(category);
        }
        public ResponseMsg ResponseCategoryUpdateAction(CategoryData category)
        {
            return ExecuteCategoryUpdateAction(category);
        }
        public ResponseMsg ResponseCategoryDeleteAction(int id)
        {
            return ExecuteCategoryDeleteAction(id);
        }
    }
}