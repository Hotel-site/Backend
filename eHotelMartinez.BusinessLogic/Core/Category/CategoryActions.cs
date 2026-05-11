using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.Category;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.BusinessLogic.Core.Category
{
    public class CategoryActions
    {
        protected List<CategoryDTO> ExecuteGetAllCategoriesAction()
        {
            using (var db = new CategoryContext())
            {
                return db.Categories
                .Where(c => c.IsActive)
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();
            }
        }
        protected CategoryDTO ExecuteGetCategoryByIdAction(int id)
        {
            using (var db = new CategoryContext())
            {
                var category = db.Categories.FirstOrDefault(c => c.Id == id);
                
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
        protected ResponseAction ExecuteCategoryCreateAction(CreateCategoryDTO category)
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
            using (var db = new CategoryContext())
            {
                var existCategory = db.Categories.FirstOrDefault(c => c.Name.ToLower() == name);
                
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
        protected ResponseMsg ExecuteCategoryUpdateAction(CategoryData category)
        {
            using (var db = new CategoryContext())
            {
                var existCategory = db.Categories.IgnoreQueryFilters().FirstOrDefault(c => c.Id == category.Id);
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
                db.SaveChanges();
                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Category updated successfully!"
                };
            }
        }
        protected ResponseMsg ExecuteCategoryDeleteAction(int id)
        {
            using (var db = new CategoryContext())
            {
                var existCategory = db.Categories.FirstOrDefault(u => u.Id == id);
                
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
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Category was deactivated!"
                };
            }
        }
    }
}