using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Entities.Category;
using eHotelMartinez.Domain.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    namespace eHotelMartinez.Api.Controller
    {
        [Route("api/category")]
        [ApiController]
        [Authorize]
        public class CategoryController : ControllerBase
        {
            private ICategoryActions _categoryActions;

            public CategoryController()
            {
                var bl = new BusinessLogic.BusinessLogic();
                _categoryActions = bl.GetCategoryActions();
            }

            [HttpGet("all")]
            [AllowAnonymous]
            public IActionResult GetAllCategories()
            {
                var categories = _categoryActions.GetAllCategoriesAction();
                return Ok(categories);
            }

            [HttpGet("{id}")]
            [AllowAnonymous]
            public IActionResult GetCategoryById(int id)
            {
                var category = _categoryActions.GetCategoryByIdAction(id);
                if (category == null)
                {
                    return NotFound(new { Message = "Category not found!" });
                }
                return Ok(category);
            }

            [HttpPost]
            [Authorize(Roles = "Admin")]
            public IActionResult CategoryCreate([FromBody] CreateCategoryDTO category)
            {
                var NewCategory = _categoryActions.ResponseCategoryCreateAction(category);
                if (NewCategory.IsSuccess == false)
                {
                    return BadRequest(NewCategory);
                }
                return Ok(NewCategory);
            }

            [HttpPut("{id}")]
            [Authorize(Roles = "Admin")]
            public IActionResult CategoryUpdate(int id, [FromBody] CategoryData category)
            {
                category.Id = id;
                var UpdateCategory = _categoryActions.ResponseCategoryUpdateAction(category);
                if (UpdateCategory.IsSuccess == false)
                {
                    return BadRequest(UpdateCategory);
                }
                return Ok(UpdateCategory);
            }

            [HttpDelete("{id}")]
            [Authorize(Roles = "Admin")]
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