using eHotelMartinez.Api.Filters;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    namespace eHotelMartinez.Api.Controller
    {
        [Route("api/category")]
        [ApiController]
        public class CategoryController : ControllerBase
        {
            private ICategoryActions _categoryActions;

            public CategoryController()
            {
                var bl = new BusinessLogic.BusinessLogic();
                _categoryActions = bl.GetCategoryActions();
            }

            [HttpGet("all")]
            public IActionResult GetAllCategories()
            {
                var categories = _categoryActions.GetAllCategoriesAction();
                return Ok(categories);
            }

            [HttpGet("{id}")]
            public IActionResult GetCategoryById(int id)
            {
                var category = _categoryActions.GetCategoryByIdAction(id);
                if (category == null)
                {
                    return NotFound(new { Message = "Category not found!" });
                }
                return Ok(category);
            }

            [AdminOnly]
            [HttpPost]
            public IActionResult CategoryCreate([FromBody] CreateCategoryDTO category)
            {
                var NewCategory = _categoryActions.ResponseCategoryCreateAction(category);
                if (NewCategory.IsSuccess == false)
                {
                    return BadRequest(NewCategory);
                }
                return Ok(NewCategory);
            }

            [AdminOnly]
            [HttpPut("{id}")]
            public IActionResult CategoryUpdate(int id, [FromBody] CategoryDTO category)
            {
                category.Id = id;
                var UpdateCategory = _categoryActions.ResponseCategoryUpdateAction(category);
                if (UpdateCategory.IsSuccess == false)
                {
                    return BadRequest(UpdateCategory);
                }
                return Ok(UpdateCategory);
            }

            [AdminOnly]
            [HttpDelete("{id}")]
            public IActionResult CategoryDelete(int id)
            {
                var categoryDel = _categoryActions.ResponseCategoryDeleteAction(id);
                if (categoryDel.IsSuccess == false)
                {
                    return BadRequest(categoryDel);
                }
                return Ok(categoryDel);
            }
        }
    }
}