using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Category;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface ICategoryActions
    {
        List<CategoryDTO> GetAllCategoriesAction();
        CategoryDTO GetCategoryByIdAction(int id);
        ResponseMsg ResponseCategoryCreateAction(CreateCategoryDTO category);
        ResponseMsg ResponseCategoryUpdateAction(CategoryDTO category);
        ResponseMsg ResponseCategoryDeleteAction(int id);
    }
}
