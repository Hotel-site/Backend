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
        public IActionResult GetAllAttractions()
        {
            var attractions = _attractionActions.GetAllAttractions();
            return Ok(attractions);
        }

        [HttpGet("{id}")]
        public IActionResult GetAttractionById(int id)
        {
            var attraction = _attractionActions.GetAttractionById(id);
            if (attraction == null)
            {
                return NotFound(new { Message = $"Attraction with ID {id} Not Found!" });
            }
            return Ok(attraction);
        }

        [AdminOnly]
        [HttpPost]
        public IActionResult CreateAttraction([FromBody] CreateAttractionDTO attraction)
        {
            var response = _attractionActions.CreateAttraction(attraction);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AdminOnly]
        [HttpPut("{id}")]
        public IActionResult UpdateAttraction(int id, [FromBody] UpdateAttractionDTO attraction)
        {
            attraction.Id = id;
            var response = _attractionActions.UpdateAttraction(attraction);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AdminOnly]
        [HttpDelete("{id}")]
        public IActionResult DeleteAttraction(int id)
        {
            var response = _attractionActions.DeleteAttraction(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
