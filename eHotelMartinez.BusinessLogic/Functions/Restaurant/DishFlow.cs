using eHotelMartinez.BusinessLogic.Core.Restaurant;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Restaurant;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Functions.Restaurant
{
    public class DishFlow : DishActions, IDishActions
    {
        public async Task<List<DishDTO>> GetAllDishesAction()
        {
            return await ExecuteGetAllDishes();
        }
        public async Task<DishDTO> GetDishByIdAction(int id)
        {
            return await ExecuteGetDishById(id);
        }
        public async Task<ResponseAction> ResponseDishCreateAction(CreateDishDTO dish)
        {
            return await ExecuteCreateDish(dish);
        }
        public async Task<ResponseMsg> ResponseDishUpdateAction(DishDTO dish)
        {
            return await ExecuteUpdateDish(dish);
        }
        public async Task<ResponseMsg> ResponseDishDeleteAction(int id)
        {
            return await ExecuteDeleteDish(id);
        }
    }
}
