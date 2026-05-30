using eHotelMartinez.Domain.Models.Restaurant;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IDishActions
    {
        Task<List<DishDTO>> GetAllDishesAction();
        Task<DishDTO> GetDishByIdAction(int id);
        Task<ResponseAction> ResponseDishCreateAction(CreateDishDTO dish);
        Task<ResponseMsg> ResponseDishUpdateAction(DishDTO dish);
        Task<ResponseMsg> ResponseDishDeleteAction(int id);
    }
}