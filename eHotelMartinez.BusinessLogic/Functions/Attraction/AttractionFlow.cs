using eHotelMartinez.BusinessLogic.Core.Attraction;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Attraction;

namespace eHotelMartinez.BusinessLogic.Functions.Attraction
{
    public class AttractionFlow : AttractionActions, IAttractionActions
    {
        public async Task<List<AttractionDTO>> GetAllAttractions()
        {
            return await ExecuteGetAllAttractions();
        }
        public async Task<AttractionDTO> GetAttractionById(int id)
        {
            return await ExecuteGetAttractionById(id);
        }
        public async Task<ResponseAction> CreateAttraction(CreateAttractionDTO attraction)
        {
            return await ExecuteCreateAttraction(attraction);
        }
        public async Task<ResponseMsg> UpdateAttraction(UpdateAttractionDTO attraction)
        {
            return await ExecuteUpdateAttraction(attraction);
        }
        public async Task<ResponseMsg> DeleteAttraction(int id)
        {
            return await ExecuteDeleteAttraction(id);
        }   

    }
}
