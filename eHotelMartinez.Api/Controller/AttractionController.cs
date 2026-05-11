using Microsoft.AspNetCore.Mvc;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Attraction;
using eHotelMartinez.Domain.Entities.Attraction;
using eHotelMartinez.Api.Filters;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/attraction")]
    [ApiController]
    public class AttractionController : ControllerBase
    {
        private IAttractionActions _attractionActions;

        public AttractionController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _attractionActions = bl.GetAttractionActions();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAttractions()
        {
            var attractions = await _attractionActions.GetAllAttractions();
            return Ok(attractions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttractionById(int id)
        {
            var attraction = await _attractionActions.GetAttractionById(id);
            if (attraction == null)
            {
                return NotFound(new { Message = $"Attraction with ID {id} Not Found!" });
            }
            return Ok(attraction);
        }

        [AdminOnly]
        [HttpPost]
        public async Task<IActionResult> CreateAttraction([FromBody] CreateAttractionDTO attraction)
        {
            var response = await _attractionActions.CreateAttraction(attraction);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AdminOnly]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttraction(int id, [FromBody] UpdateAttractionDTO attraction)
        {
            attraction.Id = id;
            var response = await _attractionActions.UpdateAttraction(attraction);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AdminOnly]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttraction(int id)
        {
            var response = await _attractionActions.DeleteAttraction(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
