using eHotelMartinez.Domain.Models.Attraction;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Entities.Attraction;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IAttractionActions
    {
        List<AttractionDTO> GetAllAttractions();
        AttractionDTO GetAttractionById(int id);
        ResponseAction CreateAttraction(CreateAttractionDTO attraction);
        ResponseMsg UpdateAttraction(UpdateAttractionDTO attraction);
        ResponseMsg DeleteAttraction(int id);
    }
}
