using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.Category;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.BusinessLogic.Core.Category
{
    public class CategoryActions
    {
        protected async Task<List<CategoryDTO>> ExecuteGetAllCategoriesAction()
        {
            await using (var db = new CategoryContext())
            {
                return await db.Categories
                .Where(c => c.IsActive)
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
            }
        }
        protected async Task<CategoryDTO> ExecuteGetCategoryByIdAction(int id)
        {
            await using (var db = new CategoryContext())
            {
                var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
                
                if (category == null)
                {
                    return null;
                }
                return new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                };
            }
        }
        protected async Task<ResponseAction> ExecuteCategoryCreateAction(CreateCategoryDTO category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Name of Category isn't be empty!"
                };
            }
            var name = category.Name.ToLower();
            await using (var db = new CategoryContext())
            {
                var existCategory = await db.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == name);
                
                if (existCategory != null)
                {
                    return new ResponseAction
                    {
                        IsSuccess = false,
                        Message = "Category is already exist!",
                        Id = existCategory.Id
                    };
                }
            }

            var Category = new CategoryData
            {
                Name = category.Name,
                IsActive = true
            };
            using (var db = new CategoryContext())
            {
                db.Categories.Add(Category);
                db.SaveChanges();   
            }
            return new ResponseAction
            {
                IsSuccess = true,
                Message = "Category created successfully!",
                Id = Category.Id
            };
        }
        protected async Task<ResponseMsg> ExecuteCategoryUpdateAction(CategoryData category)
        {
            await using (var db = new CategoryContext())
            {
                var existCategory = await db.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == category.Id);
                if (existCategory == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "The Category doesn't exist!"
                    };
                }
                if (string.IsNullOrWhiteSpace(category.Name))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Name of Category isn't be empty!"
                    };
                }
                existCategory.Name = category.Name;
                existCategory.IsActive = category.IsActive;
                await db.SaveChangesAsync();
                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Category updated successfully!"
                };
            }
        }
        protected async Task<ResponseMsg> ExecuteCategoryDeleteAction(int id)
        {
            await using (var db = new CategoryContext())
            {
                var existCategory = await db.Categories.FirstOrDefaultAsync(u => u.Id == id);
                
                if (existCategory == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "The Category doesn't exist!"
                    };
                }

                var withProducts = db.Products.FirstOrDefault(p => p.CategoryId == id);
                if (withProducts != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "This Category can't be deleted"
                    };
                }

                var withAttractions = db.Attractions.FirstOrDefault(p => p.CategoryId == id);
                if (withAttractions != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "This Category can't be deleted"
                    };
                }

                existCategory.IsActive = false;
                await db.SaveChangesAsync();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Category was deactivated!"
                };
            }
        }
    }
}