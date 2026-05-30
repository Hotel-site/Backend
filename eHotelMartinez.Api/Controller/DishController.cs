using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class DishController : ControllerBase
    {
        public IDishActions _dishActions;
        public DishController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _dishActions = bl.GetDishActions();
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDishes()
        {
            var dishes = await _dishActions.GetAllDishesAction();
            return Ok(dishes);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDishById(int id)
        {
            var dish = await _dishActions.GetDishByIdAction(id);

            if (dish == null)
            {
                return NotFound(new
                {
                    Message = $"Dish with ID {id} Not Found!"
                });
            }
            return Ok(dish);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDish([FromBody] CreateDishDTO dish)
        {
            var response = await _dishActions.ResponseDishCreateAction(dish);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDish(int id, [FromBody] DishDTO dish)
        {
            dish.Id = id;
            var response = await _dishActions.ResponseDishUpdateAction(dish);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var response = await _dishActions.ResponseDishDeleteAction(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
