using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/favorite")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private IFavoriteAction _favoriteActions;

        public FavoriteController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _favoriteActions = bl.GetFavoriteActions();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetFavoritesByUser(int userId)
        {
            var favorites = await _favoriteActions.GetFavoritesByUserId(userId);
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] CreateFavoriteDTO favorite)
        {
            var response = await _favoriteActions.AddFavorite(favorite);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{favoriteId}")]
        public async Task<IActionResult> RemoveFavorite(int favoriteId)
        {
            var response = await _favoriteActions.RemoveFavorite(favoriteId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
