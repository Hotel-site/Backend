using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Category;
using eHotelMartinez.Domain.Entities.Category;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface ICategoryActions
    {
        List<CategoryDTO> GetAllCategoriesAction();
        CategoryDTO GetCategoryByIdAction(int id);
        ResponseAction ResponseCategoryCreateAction(CreateCategoryDTO category);
        ResponseMsg ResponseCategoryUpdateAction(CategoryData category);
        ResponseMsg ResponseCategoryDeleteAction(int id);
    }
}
