using eHotelMartinez.Domain.Entities.Restaurant;
using eHotelMartinez.Domain.Models.Restaurant;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.BusinessLogic.Core.Restaurant
{
    public class DishActions
    {
        protected async Task<List<DishDTO>> ExecuteGetAllDishes()
        {
            await using var db = new DishContext();

            var dishes = await db.Dishes
                .AsNoTracking()
                .Where(d => d.IsActive)
                .ToListAsync();

            return dishes.Select(d => new DishDTO
            {
                Id = d.Id,
                DayOfWeek = d.DayOfWeek,
                Meal = d.Meal,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                IsActive = d.IsActive
            }).ToList();
        }

        protected async Task<DishDTO> ExecuteGetDishById(int id)
        {
            await using var db = new DishContext();

            var d = await db.Dishes
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);

            if (d == null)
                throw new Exception("Dish not found");

            return new DishDTO
            {
                Id = d.Id,
                DayOfWeek = d.DayOfWeek,
                Meal = d.Meal,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                IsActive = d.IsActive
            };
        }

        protected async Task<ResponseAction> ExecuteCreateDish(CreateDishDTO createDishDTO)
        {
            await using var db = new DishContext();

            if (string.IsNullOrWhiteSpace(createDishDTO.Name))
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Dish name is required."
                };

            if (createDishDTO.Price < 0)
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Price cannot be negative."
                };

            if (!Enum.IsDefined(createDishDTO.DayOfWeek))
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Invalid day of the week."
                };
            if (!Enum.IsDefined(createDishDTO.Meal))
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Invalid meal type."
                };

            var existingDish = await db.Dishes
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Name == createDishDTO.Name && d.DayOfWeek == createDishDTO.DayOfWeek && d.Meal == createDishDTO.Meal);

            if (existingDish != null)
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "A dish with the same name, day, and meal already exists.",
                    Id = existingDish.Id
                };

            var dish = new DishData
            {
                DayOfWeek = createDishDTO.DayOfWeek,
                Meal = createDishDTO.Meal,
                Name = createDishDTO.Name,
                Description = createDishDTO.Description,
                Price = createDishDTO.Price,
                IsActive = createDishDTO.IsActive
            };

            db.Dishes.Add(dish);
            await db.SaveChangesAsync();

            return new ResponseAction
            {
                IsSuccess = true,
                Message = "Dish created successfully.",
                Id = dish.Id
            };
        }
        protected async Task<ResponseMsg> ExecuteUpdateDish(DishDTO dish)
        {
            await using var db = new DishContext();
            var existingDish = await db.Dishes.FirstOrDefaultAsync(d => d.Id == dish.Id);
            if (existingDish == null)
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Dish not found."
                };
            if (string.IsNullOrWhiteSpace(dish.Name))
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Dish name is required."
                };
            if (dish.Price < 0)
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Price cannot be negative."
                };
            if (!Enum.IsDefined(dish.DayOfWeek))
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Invalid day of the week."
                };
            if (!Enum.IsDefined(dish.Meal))
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Invalid meal type."
                };
            existingDish.DayOfWeek = dish.DayOfWeek;
            existingDish.Meal = dish.Meal;
            existingDish.Name = dish.Name;
            existingDish.Description = dish.Description;
            existingDish.Price = dish.Price;
            existingDish.IsActive = dish.IsActive;

            await db.SaveChangesAsync();
            
            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Dish updated successfully."
            };
        }

        protected async Task<ResponseMsg> ExecuteDeleteDish(int id)
        {
            await using var db = new DishContext();
            
            var dish = await db.Dishes.FirstOrDefaultAsync(d => d.Id == id);

            if (dish == null)
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Dish not found."
                };

            dish.IsActive = false;

            await db.SaveChangesAsync();

            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "Dish deleted successfully."
            };  
        }
    }
}
