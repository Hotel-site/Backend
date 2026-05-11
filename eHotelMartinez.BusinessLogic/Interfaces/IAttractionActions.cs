using eHotelMartinez.Domain.Models.Attraction;
using eHotelMartinez.Domain.Models.Base;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IAttractionActions
    {
        Task<List<AttractionDTO>> GetAllAttractions();
        Task<AttractionDTO> GetAttractionById(int id);
        Task<ResponseAction> CreateAttraction(CreateAttractionDTO attraction);
        Task<ResponseMsg> UpdateAttraction(UpdateAttractionDTO attraction);
        Task<ResponseMsg> DeleteAttraction(int id);
    }
}
